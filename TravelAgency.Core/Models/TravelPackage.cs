

namespace TravelAgency.Core.Models
{
    public class TravelPackage
    {
        public Guid Id { get; set; }
        public string PackageName { get; set; } = string.Empty;
        public string PackageDescription { get; set; } = string.Empty;
        public int PackagePrice { get; set; }
        public int MaxCapacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Price { get; internal set; }

        // Link to Flight
        public Guid? FlightId { get; set; }
        public Flight? Flight { get; set; }

        // Link to Hotel
        public Guid? HotelId { get; set; }
        public Hotel? Hotel { get; set; }
    }
}
