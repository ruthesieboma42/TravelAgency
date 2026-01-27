using Microsoft.AspNetCore.Mvc;
using TravelAgency.Core.DTOs;
using TravelAgency.Core.Interfaces;
using TravelAgency.Core.Models;

namespace TravelAgency.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly IAmadeusService _amadeusService;

        public HotelsController(IHotelService hotelService, IAmadeusService amadeusService)
        {
            _hotelService = hotelService;
            _amadeusService = amadeusService;
        }


        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var hotels = await _hotelService.GetHotelsWithAvailableRoomsAsync();
        //    return Ok(hotels);
        //}

        //[HttpPost("admin/add")]
        //public async Task<IActionResult> AddHotel([FromBody] Hotel hotel)
        //{
        //    await _hotelService.AddHotelAsync(hotel);
        //    return Ok(hotel);
        //}

        //[HttpPost("{hotelId}/rooms")]
        //public async Task<IActionResult> AddRoom(Guid hotelId, [FromBody] Room room)
        //{
        //    room.HotelId = hotelId;
        //    try
        //    {
        //        await _hotelService.AddRoomAsync(room);
        //        return Ok(new { message = "Room added successfully!", roomId = room.Id });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        [HttpGet("hotels-search")]
        public async Task<IActionResult> SearchLiveHotels(
      [FromQuery] string cityCode,
      [FromQuery] string checkInDate,
      [FromQuery] string checkOutDate,
      [FromQuery] int adults = 1)
        {
            try
            {
                var results = await _amadeusService.SearchHotelsAsync(cityCode, checkInDate, checkOutDate, adults);

                if (results == null)
                {
                    return StatusCode(500, new
                    {
                        error = "API returned null response",
                        message = "Check the application logs for details from Amadeus API"
                    });
                }

                if (results.Data == null || !results.Data.Any())
                {
                    return NotFound("No hotel offers found for the specified criteria.");
                }

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "External API Error",
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        [HttpPost("select-hotel")]
        public async Task<IActionResult> SelectHotel([FromBody] HotelOfferDto offer)
        {
            var savedHotel = await _amadeusService.SyncHotelToDbAsync(offer);

            return Ok(new { HotelId = savedHotel.Id, Message = "Hotel persisted to local DB" });
        }
    }
}