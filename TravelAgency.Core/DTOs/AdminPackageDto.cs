using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Core.DTOs
{
    internal class AdminPackageDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public int MaxCapacity { get; set; }
        public Guid FlightId { get; set; }
        public Guid HotelId { get; set; }
    }
}
