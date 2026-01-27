using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Core.DTOs
{
    internal class CreateBookingDto
    {
        public Guid UserId { get; set; }
        public Guid PackageId { get; set; }
        public Guid FlightId { get; set; }
        public Guid RoomId { get; set; }
    }
}
