using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Security.Cryptography.Pkcs;

namespace VillaManagementWeb.Models
{
    public class VillaDbContext : DbContext
    {
        public VillaDbContext(DbContextOptions<VillaDbContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourBooking> TourBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình Fluent API
            modelBuilder.Entity<Room>().Property(r => r.PricePerNight).HasPrecision(18, 2);
            modelBuilder.Entity<Booking>().Property(b => b.TotalAmount).HasPrecision(18, 2);
            modelBuilder.Entity<Ticket>().Property(t => t.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Tour>().Property(t => t.PricePerPerson).HasPrecision(18, 2);
            modelBuilder.Entity<Room>().HasIndex(r => r.RoomNumber).IsUnique();

            // 1. Seed Data cho Room (Bổ sung RatingStars) 
            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    RoomNumber = "V01",
                    Type = "Villa",
                    PricePerNight = 5000000,
                    Capacity = 10,
                    RatingStars = 5,
                    IsActive = true,
                    Description = "Villa view rừng cực đẹp",
                    SquareFootage = 150,
                    HasWifi = true,
                    HasBreakfast = true,
                    HasPool = true,
                    HasTowel = true
                },
                new Room
                {
                    Id = 2,
                    RoomNumber = "B01",
                    Type = "Bungalow",
                    PricePerNight = 1500000,
                    Capacity = 2,
                    RatingStars = 4,
                    IsActive = true,
                    Description = "Không gian lãng mạn",
                    HasWifi = true,
                    HasBreakfast = true,
                    HasPool = true,
                    HasTowel = true
                }
            );

            // 2. Seed Data cho Booking (Fix lỗi CustomerPhone, PaymentMethod, v.v.) [cite: 14, 21]
            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    Id = 1,
                    RoomId = 1,
                    CustomerName = "Nguyễn Văn A",
                    CustomerPhone = "0901234567", // Bổ sung [cite: 14]
                    CustomerEmail = "a@gmail.com",
                    AdultsCount = 2,               // Bổ sung [cite: 14]
                    ChildrenCount = 1,             // Bổ sung [cite: 14]
                    CheckIn = DateTime.Now.AddDays(2),
                    CheckOut = DateTime.Now.AddDays(5),
                    TotalAmount = 15000000,
                    Status = "Confirmed",
                    PaymentMethod = "Credit Card"  // Bổ sung [cite: 14]
                }
            );
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1, // Đảm bảo ID này tồn tại 
                    Title = "Đêm Nhạc Dưới Trăng",
                    Description = "Show ca nhạc acoustic cực chill tại Rose Villa",
                    EventDate = DateTime.Now.AddDays(15),
                    Location = "Sân khấu ngoài trời",
                    TotalTickets = 100,
                    ImageUrl = "/images/events/event1.jpg"
                }
            );
            // 3. Seed Data cho Ticket (Bổ sung TicketType, QRCode) [cite: 19, 21]
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket
                {
                    Id = 1,
                    EventId = 1,
                    CustomerName = "Trần Thị B",
                    CustomerEmail = "b@gmail.com",
                    TicketType = "VIP",            // Bổ sung [cite: 19]
                    Quantity = 2,
                    Price = 500000,
                    BookingDate = DateTime.Now,
                    QRCode = "QR_EVT_001",         // Bổ sung [cite: 19]
                    IsUsed = false
                }
            );
            modelBuilder.Entity<Tour>().HasData(
                new Tour
                {
                    Id = 1,
                    TourName = "Trekking Rừng Quốc Gia Ba Vì",
                    Description = "Khám phá vẻ đẹp hoang sơ của núi rừng Ba Vì cùng hướng dẫn viên bản địa.",
                    PricePerPerson = 500000,
                    DurationHours = 6,
                    ImageUrl = "/images/tours/trekking-bavi.jpg"
                }
            );
            // 4. Seed Data cho TourBooking (Bổ sung ContactInfo) [cite: 20, 21]
            modelBuilder.Entity<TourBooking>().HasData(
                new TourBooking
                {
                    Id = 1,
                    TourId = 1,
                    CustomerName = "Lê Văn C",
                    ContactInfo = "0988777666",    // Bổ sung 
                    TourDate = DateTime.Now.AddDays(10),
                    NumberOfPeople = 4,
                    TotalPrice = 2000000,
                    Status = "Pending"
                }
            );

            // 5. Cấu hình RoomImage 
            modelBuilder.Entity<RoomImage>()
                .HasOne(ri => ri.Room)
                .WithMany(r => r.RoomImages)
                .HasForeignKey(ri => ri.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RoomImage>().HasData(
                new RoomImage { Id = 1, RoomId = 1, ImageUrl = "/images/rooms/villa1-1.jpg" }
            );
            modelBuilder.Entity<News>().HasData(
                new News { Id = 1, Title = "Music Concert Night", Category = "Sự kiện âm nhạc", ImageUrl = "/images/news/news1.png" },
                new News { Id = 2, Title = "New Villa Opening", Category = "Tin tức", ImageUrl = "/images/news/news2.jpg" }
            );
        }
    }
}
