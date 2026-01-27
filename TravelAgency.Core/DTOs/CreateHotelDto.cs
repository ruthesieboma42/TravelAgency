using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Core.DTOs
{
    internal class CreateHotelDto
    {
        public string Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

    }
}
