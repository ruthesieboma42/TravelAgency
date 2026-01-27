

namespace TravelAgency.Core.Models;

public class Booking
{
    public Guid Id { get; set; }
    public Guid UsersId { get; set; }

    public Guid TravelPackageId { get; set; }
    public TravelPackage? TravelPackage { get; set; }

    public Guid? FlightId { get; set; }
    public Flight? Flight { get; set; }

    public Guid? HotelId { get; set; }
    public Hotel? Hotel { get; set; }

    public Guid? RoomId { get; set; }
    public Room? Room { get; set; }

    public int TotalPrice { get; set; }

    public required string Status { get; set; }
    public DateTime BookingDate { get; set; } = DateTime.UtcNow;

}
