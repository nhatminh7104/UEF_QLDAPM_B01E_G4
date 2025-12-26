using Microsoft.EntityFrameworkCore;

namespace VillaManagementWeb.Models
{
    public class VillaDbContext : DbContext
    {
        public VillaDbContext(DbContextOptions<VillaDbContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourBooking> TourBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Cấu hình Fluent API để tối ưu SQL Server
            modelBuilder.Entity<Room>().Property(r => r.PricePerNight).HasPrecision(18, 2);
            modelBuilder.Entity<Booking>().Property(b => b.TotalAmount).HasPrecision(18, 2);
            modelBuilder.Entity<Ticket>().Property(t => t.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Tour>().Property(t => t.PricePerPerson).HasPrecision(18, 2);

            // Đảm bảo RoomNumber là duy nhất
            modelBuilder.Entity<Room>().HasIndex(r => r.RoomNumber).IsUnique();

            //Seed Data
            // 1. Seed Data cho Room (Phòng)
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, RoomNumber = "V01", Type = "Villa", PricePerNight = 5000000, Capacity = 10, IsActive = true },
                new Room { Id = 2, RoomNumber = "B01", Type = "Bungalow", PricePerNight = 1500000, Capacity = 2, IsActive = true },
                new Room { Id = 3, RoomNumber = "V02", Type = "Villa", PricePerNight = 4500000, Capacity = 8, IsActive = true }
            );

            // 2. Seed Data cho Event (Sự kiện ca nhạc)
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Title = "Đêm Nhạc Dưới Trăng",
                    Description = "Show ca nhạc acoustic cực chill tại Rose Villa",
                    EventDate = DateTime.Now.AddDays(15),
                    Location = "Sân khấu ngoài trời",
                    TotalTickets = 100
                }
            );

            // 3. Seed Data cho Tour (Trải nghiệm)
            modelBuilder.Entity<Tour>().HasData(
                new Tour
                {
                    Id = 1,
                    TourName = "Trekking Rừng Quốc Gia Ba Vì",
                    Description = "Khám phá vẻ đẹp thiên nhiên",
                    PricePerPerson = 500000,
                    DurationHours = 6
                }
            );
        }
    }
}
