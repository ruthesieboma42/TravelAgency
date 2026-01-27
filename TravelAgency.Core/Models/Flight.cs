namespace TravelAgency.Core.Models;

public class Flight
{
    public Guid Id { get; set; }
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public decimal Price { get; set; }
    public string Airline { get; set; } = string.Empty;
    public string FlightNumber { get; set; } = string.Empty;

    public bool IsLiveApiSourced { get; set; }
    public string Data { get; set; } = string.Empty;
}