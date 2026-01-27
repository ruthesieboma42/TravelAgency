using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using TravelAgency.Core.DTOs;
using TravelAgency.Core.Interfaces;
using TravelAgency.Core.Models;
using TravelAgency.Data;

namespace TravelAgency.Services.Services
{
    public class AmadeusService : IAmadeusService
    {
        private readonly HttpClient _httpClient;
        private readonly AmadeusSettings _settings;
        private string? _cachedToken;
        private DateTime _tokenExpiry = DateTime.MinValue;
        private readonly ApplicationDbContext _context;

        public AmadeusService(HttpClient httpClient, IOptions<AmadeusSettings> options, ApplicationDbContext context)
        {
            _httpClient = httpClient;
            _settings = options.Value;
            _context = context;
        }

        private async Task<string> GetAccessTokenAsync()
        {
            if (!string.IsNullOrEmpty(_cachedToken) && DateTime.UtcNow < _tokenExpiry)
            {
                return _cachedToken;
            }

            var requestData = new List<KeyValuePair<string, string>>
            {
                new("grant_type", "client_credentials"),
                new("client_id", _settings.ApiKey),
                new("client_secret", _settings.ApiSecret)
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "v1/security/oauth2/token")
            {
                Content = new FormUrlEncodedContent(requestData)
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(content);

            if (tokenResponse == null) throw new Exception("Failed to deserialize Amadeus token.");

            _cachedToken = tokenResponse.AccessToken;
            _tokenExpiry = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn - 30);

            return _cachedToken;
        }


        public async Task<AmadeusFlightResponse?> SearchFlightsAsync(string origin, string destination, string date)
        {
            var token = await GetAccessTokenAsync();
            var url = $"v2/shopping/flight-offers?originLocationCode={origin}&destinationLocationCode={destination}&departureDate={date}&adults=1&max=5";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<AmadeusFlightResponse>(content, options);
        }

        public async Task<Flight> SyncFlightToDbAsync(FlightOfferDto offer)
        {
            var firstSegment = offer.Itineraries[0].Segments[0];
            var lastSegment = offer.Itineraries[0].Segments.Last();

            var localFlight = new Flight
            {
                Id = Guid.NewGuid(),
                Origin = firstSegment.Departure.IataCode,
                Destination = lastSegment.FlightArrival.IataCode,
                DepartureTime = DateTime.SpecifyKind(firstSegment.Departure.At, DateTimeKind.Utc),
                ArrivalTime = DateTime.SpecifyKind(lastSegment.FlightArrival.At, DateTimeKind.Utc),
                Airline = firstSegment.CarrierCode,
                FlightNumber = firstSegment.Number,
                IsLiveApiSourced = true
            };

            _context.Flights.Add(localFlight);
            await _context.SaveChangesAsync();
            return localFlight;
        }

        public async Task<HotelSearchResponseDto?> SearchHotelsAsync(string cityCode, string checkInDate, string checkOutDate, int adults)
        {
            var token = await GetAccessTokenAsync();

            // Step 1: Get hotel IDs by city code
            var hotelIds = await GetHotelIdsByCityAsync(cityCode, token);

            if (hotelIds == null || !hotelIds.Any())
            {
                // Return empty result if no hotels found
                return new HotelSearchResponseDto { Data = new List<HotelOfferDto>() };
            }

            // Step 2: Get hotel offers using the hotel IDs
            var hotelIdsParam = string.Join(",", hotelIds.Take(20)); // Limit to 20 hotels for performance
            var url = $"v3/shopping/hotel-offers?hotelIds={hotelIdsParam}&checkInDate={checkInDate}&checkOutDate={checkOutDate}&adults={adults}&roomQuantity=1&bestRateOnly=true";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Amadeus API Error {response.StatusCode}: {content}");
            }

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<HotelSearchResponseDto>(content, options);
        }

        private async Task<List<string>?> GetHotelIdsByCityAsync(string cityCode, string token)
        {
            var url = $"v1/reference-data/locations/hotels/by-city?cityCode={cityCode}&radius=5&radiusUnit=KM&hotelSource=ALL";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Amadeus API Error (Hotel Lookup) {response.StatusCode}: {content}");
            }

            // Parse the response to extract hotel IDs
            using var doc = JsonDocument.Parse(content);
            var hotelIds = doc.RootElement
                .GetProperty("data")
                .EnumerateArray()
                .Select(hotel => hotel.GetProperty("hotelId").GetString())
                .Where(id => !string.IsNullOrEmpty(id))
                .ToList();

            return hotelIds!;
        }


        public async Task<Hotel> SyncHotelToDbAsync(HotelOfferDto offer)
        {
            var firstOffer = offer.Offers[0];
            var hotelId = Guid.NewGuid();

            var localHotel = new Hotel
            {
                Id = hotelId,
                Name = offer.Hotel.Name,
                CityCode = offer.Hotel.CityCode,
                Address = offer.Hotel.HotelId,
                PricePerNight = decimal.Parse(firstOffer.Price.Total),
                CheckInDate = DateTime.SpecifyKind(DateTime.Parse(firstOffer.CheckInDate), DateTimeKind.Utc),
                CheckOutDate = DateTime.SpecifyKind(DateTime.Parse(firstOffer.CheckOutDate), DateTimeKind.Utc),
                IsLiveApiSourced = true,
                AmadeusHotelId = offer.Hotel.HotelId,
                Rooms = new List<Room>
                {
                    new Room
                    {
                        Id = Guid.NewGuid(),
                        HotelId = hotelId,
                        RoomType = firstOffer.Room.Type,
                        Description = firstOffer.Room.Description.Text,
                        PricePerNight = decimal.Parse(firstOffer.Price.Total),
                        IsAvailable = true
                    }
                }
            };


            _context.Hotels.Add(localHotel);
            await _context.SaveChangesAsync();

            return localHotel;


        }

       

       
    } }