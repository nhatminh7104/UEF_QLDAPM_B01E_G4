using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace VillaManagementWeb.Data
{
    // Quan trọng: Phải kế thừa IdentityDbContext với class User của bạn
    public class VillaDbContext : IdentityDbContext<User>
    {
        public VillaDbContext(DbContextOptions<VillaDbContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomBooking> RoomBookings { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourBooking> TourBookings { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình độ chính xác cho tiền tệ (Tránh lỗi Decimal trong SQL)
            modelBuilder.Entity<Room>().Property(r => r.PricePerNight).HasPrecision(18, 2);
            modelBuilder.Entity<RoomBooking>().Property(b => b.TotalAmount).HasPrecision(18, 2);
            modelBuilder.Entity<Ticket>().Property(t => t.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Tour>().Property(t => t.PricePerPerson).HasPrecision(18, 2);
            modelBuilder.Entity<TourBooking>().Property(t => t.TotalPrice).HasPrecision(18, 2);

            modelBuilder.Entity<Room>().HasIndex(r => r.RoomNumber).IsUnique();

            // Seed Data Rooms
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, RoomNumber = "V01", Type = "Villa", PricePerNight = 5000000, Capacity = 10, RatingStars = 5, IsActive = true, Description = "Villa view rừng cực đẹp", SquareFootage = 150, HasWifi = true, HasBreakfast = true, HasPool = true, HasTowel = true },
                new Room { Id = 2, RoomNumber = "B01", Type = "Bungalow", PricePerNight = 1500000, Capacity = 2, RatingStars = 4, IsActive = true, Description = "Không gian lãng mạn", HasWifi = true, HasBreakfast = true, HasPool = true, HasTowel = true }
            );

            // Seed Data RoomBookings
            modelBuilder.Entity<RoomBooking>().HasData(
                new RoomBooking { Id = 1, RoomId = 1, CustomerName = "Nguyễn Văn A", CustomerPhone = "0901234567", CustomerEmail = "a@gmail.com", AdultsCount = 2, ChildrenCount = 1, CheckIn = DateTime.Now.AddDays(2), CheckOut = DateTime.Now.AddDays(5), TotalAmount = 15000000, Status = "Confirmed", PaymentMethod = "Credit Card", CreatedAt = DateTime.Now }
            );

            modelBuilder.Entity<Event>().HasData(
                new Event { Id = 1, Title = "Đêm Nhạc Dưới Trăng", Description = "Show ca nhạc acoustic cực chill", EventDate = DateTime.Now.AddDays(15), Location = "Sân khấu ngoài trời", TotalTickets = 100, ImageUrl = "/images/events/event1.jpg" }
            );

            modelBuilder.Entity<Tour>().HasData(
                new Tour { Id = 1, TourName = "Trekking Ba Vì", Description = "Khám phá vẻ đẹp núi rừng.", PricePerPerson = 500000, DurationHours = 6, ImageUrl = "/images/tours/trekking-bavi.jpg" }
            );

            modelBuilder.Entity<News>().HasData(
                new News { Id = 1, Title = "Music Concert Night", Category = "Sự kiện âm nhạc", ImageUrl = "/images/news/news1.png" },
                new News { Id = 2, Title = "New Villa Opening", Category = "Tin tức", ImageUrl = "/images/news/news2.jpg" }
            );
        }
    }
}