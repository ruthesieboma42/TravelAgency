using TravelAgency.Core.Interfaces;
using TravelAgency.Core.Models;
using TravelAgency.Core.DTOs;

namespace TravelAgency.Services.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepo;

        public FlightService(IFlightRepository flightRepo)
        {
            _flightRepo = flightRepo;
        }

        public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
        {
            return await _flightRepo.GetAllAsync();
        }

        public async Task<Flight> CreateFlightAsync(CreateFlightDto dto)
        {
            var flight = new Flight
            {
                Id = Guid.NewGuid(),
                FlightNumber = dto.FlightNumber,
                Price = dto.Price
            };

            await _flightRepo.AddAsync(flight);
            await _flightRepo.SaveChangesAsync();
            return flight;
        }
    }
}