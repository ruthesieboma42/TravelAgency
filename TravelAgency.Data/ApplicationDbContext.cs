using Microsoft.EntityFrameworkCore;
using TravelAgency.Core.Models;

namespace TravelAgency.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<TravelPackage> TravelPackages { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Users Mapping ---
            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(u => u.Id);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Email).HasColumnName("email").IsRequired().HasMaxLength(255);
                entity.Property(u => u.PasswordHash).HasColumnName("password_hash").IsRequired();
            });

            // --- TravelPackages Mapping ---
            modelBuilder.Entity<TravelPackage>(entity =>
            {
                entity.ToTable("travel_packages");
                entity.HasKey(tp => tp.Id);
                entity.Property(tp => tp.Price).HasColumnName("price").HasColumnType("decimal(18,2)");
                entity.Property(tp => tp.PackageName).HasColumnName("package_name").IsRequired().HasMaxLength(200);
            });

            // --- Hotel Mapping ---
            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.ToTable("hotels");
                entity.HasKey(h => h.Id);
                entity.Property(h => h.Name).HasColumnName("hotel_name").IsRequired();
            });

            // --- Room Mapping ---
            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("rooms");
                entity.HasKey(r => r.Id);

                entity.HasOne(r => r.Hotel)
                      .WithMany(h => h.Rooms)
                      .HasForeignKey(r => r.HotelId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(r => r.PricePerNight).HasColumnName("price_per_night").HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.ToTable("flights");
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Price).HasColumnName("price").HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("bookings");
                entity.HasKey(b => b.Id);

                entity.Property(b => b.TotalPrice).HasColumnName("total_price").IsRequired();
                entity.Property(b => b.Status).HasColumnName("status").HasMaxLength(50);

                
                entity.HasOne(b => b.TravelPackage)
                      .WithMany()
                      .HasForeignKey(b => b.TravelPackageId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.Flight)
                      .WithMany()
                      .HasForeignKey(b => b.FlightId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(b => b.Room)
                      .WithMany()
                      .HasForeignKey(b => b.RoomId)
                      .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
