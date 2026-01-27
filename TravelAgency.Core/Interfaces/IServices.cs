
using TravelAgency.Core.DTOs;
using TravelAgency.Core.Models;
namespace TravelAgency.Core.Interfaces
{
    public interface IAuthService
    {
        Task<Users?> RegisterAsync(UserRegistrationDto dto);

        Task<string?> LoginAsync(UserLoginDto dto);
    }

    public interface IBookingService
    {
        Task<Booking?> CreateTripAsync(BookingRequestDto dto);
        Task<IEnumerable<Booking>> GetUserBookingsAsync(Guid userId);
       
    }

    public interface IHotelService
    {
        Task<IEnumerable<Hotel>> GetHotelsWithAvailableRoomsAsync();
        Task AddHotelAsync(Hotel hotel);
        Task AddRoomAsync(Room room);
        Task SaveChangesAsync();
    }

    public interface ITravelPackageService
    {
        Task<IEnumerable<TravelPackage>> GetAllAsync();
        Task<TravelPackage?> GetByIdAsync(Guid id);
        Task AddAsync(TravelPackage package);
        Task SaveChangesAsync();
    }

    public interface IFlightService
    {
        Task<IEnumerable<Flight>> GetAllFlightsAsync();
        Task<Flight> CreateFlightAsync(CreateFlightDto dto);
    }

    public interface IAmadeusService
    {
        Task<AmadeusFlightResponse?> SearchFlightsAsync(string origin, string destination, string date);
        Task<HotelSearchResponseDto> SearchHotelsAsync(string cityCode, string checkInDate, string checkOutDate, int adults);
        Task<Flight> SyncFlightToDbAsync(FlightOfferDto offer);


        Task<Hotel> SyncHotelToDbAsync(HotelOfferDto offer);

       
    }
}