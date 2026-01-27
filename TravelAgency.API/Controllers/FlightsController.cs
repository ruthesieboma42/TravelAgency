using Microsoft.AspNetCore.Mvc;
using TravelAgency.Core.DTOs;
using TravelAgency.Core.Interfaces;

namespace TravelAgency.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IAmadeusService _amadeusService;

        public FlightsController(IAmadeusService amadeusService)
        {
            _amadeusService = amadeusService;
        }

        [HttpGet("flights-search")]
        public async Task<IActionResult> SearchLiveFlights(
            [FromQuery] string origin,
            [FromQuery] string destination,
            [FromQuery] string departureDate)
        {
            try
            {
                var results = await _amadeusService.SearchFlightsAsync(origin, destination, departureDate);

                if (results == null || results.Data == null || !results.Data.Any())
                {
                    return NotFound("No flight offers found for the specified criteria.");
                }

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"External API Error: {ex.Message}");
            }
        }

        [HttpPost("select-flight")]
        public async Task<IActionResult> SelectFlight([FromBody] FlightOfferDto offer)
        {
            var savedFlight = await _amadeusService.SyncFlightToDbAsync(offer);
            return Ok(new { FlightId = savedFlight.Id, Message = "Flight persisted to local DB" });
        }
    }
}