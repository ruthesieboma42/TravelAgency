using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Core.Models
{
    public class Hotel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string CityCode { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;


        public string? AmadeusHotelId { get; set; } 

        public bool IsLiveApiSourced { get; set; } 

        public decimal PricePerNight { get; set; }

       
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        
        public List<Room> Rooms { get; set; } = new();

       
        public List<TravelPackage> TravelPackages { get; set; } = new();
    }
}