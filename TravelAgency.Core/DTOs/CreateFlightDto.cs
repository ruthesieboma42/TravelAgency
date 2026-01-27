using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Core.DTOs
{
    public class CreateFlightDto
    {
        public string FlightNumber { get; set; } = string.Empty;
        public string Airline { get; set; } = string.Empty;


        public decimal Price { get; set; }

        public string DepartureCity { get; set; } = string.Empty;
        public string ArrivalCity { get; set; } = string.Empty;

        public DateTime DepartureTime { get; set; }

    }
}
