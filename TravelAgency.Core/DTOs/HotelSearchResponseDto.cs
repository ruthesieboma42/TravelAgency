using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using static TravelAgency.Core.DTOs.SegmentDto;

namespace TravelAgency.Core.DTOs
{
    public class HotelSearchResponseDto
{
    public List<HotelOfferDto> Data { get; set; } = new();
}

public class HotelOfferDto
{
    public string Type { get; set; } = string.Empty;
    public HotelBaseDto Hotel { get; set; } = new();
    public bool Available { get; set; }
    public List<OfferDto> Offers { get; set; } = new();
}

public class HotelBaseDto
{
    public string HotelId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string CityCode { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

    public class OfferDto
    {
        public string Id { get; set; } = string.Empty;
        public string CheckInDate { get; set; } = string.Empty;
        public string CheckOutDate { get; set; } = string.Empty;
        public RoomDto Room { get; set; } = new();
        public HotelPriceDto Price { get; set; } = new();

        [JsonPropertyName("policies")]
        public JsonElement? Policies { get; set; }
    }

    public class RoomDto
{
    public string Type { get; set; } = string.Empty;
    public DescriptionDto Description { get; set; } = new();
}

public class DescriptionDto
{
    public string Text { get; set; } = string.Empty;
}

public class FlightPriceDto
{
    public string Currency { get; set; } = string.Empty;
    public string Total { get; set; } = string.Empty;
}

 public class HotelPriceDto
    {
        public string Currency { get; set; } = string.Empty;
        public string Total { get; set; } = string.Empty;
    }
    public class PolicyDto
{
    public string PaymentType { get; set; } = string.Empty;
    public string Cancellation { get; set; } = string.Empty;
}
}
