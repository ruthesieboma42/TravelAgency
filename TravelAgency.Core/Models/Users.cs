
using System.ComponentModel.DataAnnotations;


namespace TravelAgency.Core.Models
{
    public class Users
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Role { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        // Navigation property
        public List<Booking> Bookings { get; set; } = new();
    }
}
