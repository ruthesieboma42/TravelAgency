
using TravelAgency.Core.Models;

namespace TravelAgency.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<Users?> GetByIdAsync(Guid id);
        Task<Users?> GetByEmailAsync(string email);
        Task AddAsync(Users user);
        Task SaveChangesAsync();
    }

    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetUserBookingsAsync(Guid userId);
        Task AddAsync(Booking booking);
        Task SaveChangesAsync();
    }

    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetAllAsync();
        Task<Flight?> GetByIdAsync(Guid id);
        Task AddAsync(Flight flight);
        Task DeleteAsync(Guid id);
        Task SaveChangesAsync();
    }

    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetAllWithRoomsAsync();
        Task<IEnumerable<Room>> GetAvailableRoomsAsync(Guid hotelId);
        Task<Room?> GetRoomByIdAsync(Guid roomId);
        Task AddHotelAsync(Hotel hotel);
        Task AddRoomAsync(Room room);
        Task DeleteHotelAsync(Guid id);
        Task SaveChangesAsync();
    }

    public interface ITravelPackageRepository
    {
        Task<IEnumerable<TravelPackage>> GetAllAsync();
        Task<TravelPackage?> GetByIdAsync(Guid id);
        Task AddAsync(TravelPackage package);
        Task UpdateAsync(TravelPackage package);
        Task DeleteAsync(Guid id);
        Task SaveChangesAsync();
    }
}
