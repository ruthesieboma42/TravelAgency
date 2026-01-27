using TravelAgency.Core.Interfaces;
using TravelAgency.Core.Models;

namespace TravelAgency.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepo;

        public HotelService(IHotelRepository hotelRepo)
        {
            _hotelRepo = hotelRepo;
        }

        public async Task<IEnumerable<Hotel>> GetHotelsWithAvailableRoomsAsync()
        {
            var hotels = await _hotelRepo.GetAllWithRoomsAsync();
            return hotels.Where(h => h.Rooms.Any(r => r.IsAvailable));
        }

        public async Task AddHotelAsync(Hotel hotel)
        {
            await _hotelRepo.AddHotelAsync(hotel);
        }

        public async Task<Room?> GetRoomByIdAsync(Guid roomId)
        {
            return await _hotelRepo.GetRoomByIdAsync(roomId);
        }

        public async Task DeleteHotelAsync(Guid id)
        {
            await _hotelRepo.DeleteHotelAsync(id);
        }
        public async Task AddRoomAsync(Room room)
        {
            if (room.PricePerNight <= 0)
                throw new ArgumentException("Room price must be greater than zero.");

            await _hotelRepo.AddRoomAsync(room);
            await _hotelRepo.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _hotelRepo.SaveChangesAsync();
        }
    }
}