

using static TravelAgency.Core.DTOs.SegmentDto;

namespace TravelAgency.Core.DTOs;

    
        public class AmadeusFlightResponse
        {
            public List<FlightOfferDto> Data { get; set; } = new();
        }

        public class FlightOfferDto
        {
            public string Id { get; set; } = string.Empty;
            public PriceDto Price { get; set; } = new();
            public List<ItineraryDto> Itineraries { get; set; } = new();
        }

        public class ItineraryDto
        {
            public string Duration { get; set; } = string.Empty;
            public List<SegmentDto> Segments { get; set; } = new();
        }

public class SegmentDto
{
    public DepartureDto Departure { get; set; } = new();
    public ArrivalDto FlightArrival { get; set; } = new();
    public string CarrierCode { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public class FlightEndpointDto
    {
    }

    public class DepartureDto
    {
        public string IataCode { get; set; } = string.Empty;
        public string Terminal { get; set; } = string.Empty;
        public DateTime At { get; set; } 
    }

    public class ArrivalDto
    {
        public string IataCode { get; set; } = string.Empty;
        public DateTime At { get; set; } 
    }

    public class PriceDto
    {
        public string Currency { get; set; } = string.Empty;
        public string Total { get; set; } = string.Empty;
    }

}
    
//public class FlightEndPointDto
//    {
//        public string IataCode { get; set; } = string.Empty;
//        public DateTime At { get; set; }
//}

//    public class FlightDepartureDto : FlightEndPointDto { }
//    public class FlightArrivalDto : FlightEndPointDto { }

