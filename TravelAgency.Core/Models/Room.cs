using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Core.Models
{
    public class Room
    {
        [Key]
        public Guid Id { get; set; }

        public string RoomType { get; set; } = string.Empty; 

        public string Description { get; set; } = string.Empty;

        public decimal PricePerNight { get; set; }

        public bool IsAvailable { get; set; } = true;

        
        public Guid HotelId { get; set; }

        [ForeignKey("HotelId")]
        public Hotel? Hotel { get; set; }
    }
}