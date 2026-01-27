using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Core.DTOs
{
    public class BookingResponseDto
    {
        public Guid BookingId { get; set; }
        public string PackageName { get; set; } = string.Empty;
        public string FlightNumber { get; set; } = string.Empty;
        public string HotelName { get; set; } = string.Empty;
        public decimal TotalPaid { get; set; }
        public DateTime DateBooked { get; set; }
    }
}
