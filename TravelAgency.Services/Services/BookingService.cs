using TravelAgency.Core.Interfaces;
using TravelAgency.Core.Models;
using TravelAgency.Core.DTOs;

namespace TravelAgency.Services.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly ITravelPackageRepository _packageRepo;
        private readonly IFlightRepository _flightRepo;
        private readonly IHotelRepository _hotelRepo;

        public BookingService(
            IBookingRepository bookingRepo,
            ITravelPackageRepository packageRepo,
            IFlightRepository flightRepo,
            IHotelRepository hotelRepo)
        {
            _bookingRepo = bookingRepo;
            _packageRepo = packageRepo;
            _flightRepo = flightRepo;
            _hotelRepo = hotelRepo;
        }

        public async Task<Booking> CreateTripAsync(BookingRequestDto dto)
        {
            var package = await _packageRepo.GetByIdAsync(dto.PackageId);
            var flight = await _flightRepo.GetByIdAsync(dto.FlightId);
            var room = await _hotelRepo.GetRoomByIdAsync(dto.RoomId);

            if (package == null || flight == null || room == null || !room.IsAvailable)
            {
                throw new Exception("Booking failed: Invalid selection or room unavailable.");
            }

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                UsersId = dto.UserId,
                TravelPackageId = dto.PackageId,
                FlightId = dto.FlightId,
                RoomId = dto.RoomId,
                BookingDate = DateTime.UtcNow,
                TotalPrice = (int)(package.Price + flight.Price + room.PricePerNight),
                Status = "Confirmed"
            };

            room.IsAvailable = false;

            await _bookingRepo.AddAsync(booking);
            await _bookingRepo.SaveChangesAsync();
            await _hotelRepo.SaveChangesAsync();

            return booking;
        }

        public Task<Booking?> CreateTripAsync(Guid userId, Guid packageId, Guid flightId, Guid roomId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Booking>?> GetUserBookingsAsync(Guid userId)
        {
            var bookings = await _bookingRepo.GetUserBookingsAsync(userId);

            if (bookings == null || !bookings.Any())
            {
                return null;
            }

            return bookings;
        }
    }
}