using Microsoft.AspNetCore.Mvc;
using TravelAgency.Core.Interfaces;
using TravelAgency.Core.DTOs;

namespace TravelAgency.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("bookings")]
        public async Task<IActionResult> Create([FromBody] BookingRequestDto dto)
        {
           
            var result = await _bookingService.CreateTripAsync(dto);
            return Ok(result);
        }

        [HttpGet("fetch-bookings/{userId}")]
        public async Task<IActionResult> GetUserBookings(Guid userId)
        {
            return Ok(await _bookingService.GetUserBookingsAsync(userId));
        }
    }
}