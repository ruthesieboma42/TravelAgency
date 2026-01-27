
using Microsoft.EntityFrameworkCore;
using TravelAgency.Core.Interfaces;
using TravelAgency.Core.Models;


namespace TravelAgency.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context) => _context = context;
    public async Task<Users?> GetByIdAsync(Guid id) => await _context.Users.FindAsync(id);
    public async Task<Users?> GetByEmailAsync(string email) => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    public async Task AddAsync(Users user) => await _context.Users.AddAsync(user);
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}

public class BookingRepository : IBookingRepository
{
    private readonly ApplicationDbContext _context;
    public BookingRepository(ApplicationDbContext context) => _context = context;
    public async Task<IEnumerable<Booking>> GetUserBookingsAsync(Guid userId)
    {
        return await _context.Bookings
            .Include(b => b.TravelPackage)
            .Include(b => b.Flight)
            .Include(b => b.Room)
            .Where(b => b.UsersId == userId)
            .ToListAsync();
    }

    public async Task DeleteBookingAsync(Guid id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking != null)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
    }
    public async Task AddAsync(Booking booking) => await _context.Bookings.AddAsync(booking);
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}

public class HotelRepository : IHotelRepository
{
    private readonly ApplicationDbContext _context;

    public HotelRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Matches your interface: Task<IEnumerable<Hotel>> GetAllWithRoomsAsync();
    public async Task<IEnumerable<Hotel>> GetAllWithRoomsAsync()
    {
        return await _context.Hotels
            .Include(h => h.Rooms)
            .ToListAsync();
    }

    // Matches your interface: Task GetHotelByIdAsync(Guid hotelId);
    // Note: Your interface says 'Task' (void), but it should usually return 'Hotel?'
    public async Task GetHotelByIdAsync(Guid hotelId)
    {
        await _context.Hotels.FindAsync(hotelId);
    }

    public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(Guid hotelId)
    {
        return await _context.Rooms
            .Where(r => r.HotelId == hotelId && r.IsAvailable)
            .ToListAsync();
    }

    public async Task AddHotelAsync(Hotel hotel) => await _context.Hotels.AddAsync(hotel);
    public async Task AddRoomAsync(Room room) => await _context.Rooms.AddAsync(room);
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public async Task<Room?> GetRoomByIdAsync(Guid roomId)
    {
        return await _context.Rooms.FindAsync(roomId);
    }

    public async Task DeleteHotelAsync(Guid id)
    {
        var hotel = await _context.Hotels.FindAsync(id);

        if (hotel != null)
        {
            // This will also delete associated rooms if 
            // DeleteBehavior.Cascade is set in your DbContext
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
        }

    }

    public class FlightRepository : IFlightRepository
    {
        private readonly ApplicationDbContext _context;
        public FlightRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Flight>> GetAllAsync() =>
            await _context.Flights.ToListAsync();

        public async Task<Flight?> GetByIdAsync(Guid id) =>
            await _context.Flights.FindAsync(id);

        public async Task AddAsync(Flight flight) =>
            await _context.Flights.AddAsync(flight);

        public async Task DeleteAsync(Guid id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight != null) _context.Flights.Remove(flight);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }

    public class TravelPackageRepository : ITravelPackageRepository
    {
        private readonly ApplicationDbContext _context;
        public TravelPackageRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<TravelPackage>> GetAllAsync() =>
            await _context.TravelPackages.ToListAsync();

        public async Task<TravelPackage?> GetByIdAsync(Guid id) =>
            await _context.TravelPackages.FindAsync(id);

        public async Task AddAsync(TravelPackage package) =>
            await _context.TravelPackages.AddAsync(package);

        public async Task UpdateAsync(TravelPackage package)
        {
            _context.TravelPackages.Update(package);
            await Task.CompletedTask; // Update is not natively async in EF Core
        }

        public async Task DeleteAsync(Guid id)
        {
            var package = await _context.TravelPackages.FindAsync(id);
            if (package != null) _context.TravelPackages.Remove(package);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}


