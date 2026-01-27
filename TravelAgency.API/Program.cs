using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TravelAgency.Core.Interfaces;
using TravelAgency.Core.Models;
using TravelAgency.Data;
using TravelAgency.Data.Repositories;
using TravelAgency.Services;
using TravelAgency.Services.Services;
using static TravelAgency.Data.Repositories.HotelRepository;

var builder = WebApplication.CreateBuilder(args);

// 1. PostgreSQL Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// 2. Data Layer Registrations
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<ITravelPackageRepository, TravelPackageRepository>();

// 3. Service Layer Registrations
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<ITravelPackageService, TravelPackageService>();
builder.Services.AddScoped<IPasswordHasher<Users>, PasswordHasher<Users>>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAmadeusService, AmadeusService>();
builder.Services.Configure<AmadeusSettings>(builder.Configuration.GetSection("Amadeus"));


builder.Services.AddHttpClient<IAmadeusService, AmadeusService>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<AmadeusSettings>>().Value;

    client.BaseAddress = new Uri(settings.BaseUrl);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();