using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace VillaManagementWeb.Data
{
    public class VillaDbContext : IdentityDbContext<User>
    {
        public VillaDbContext(DbContextOptions<VillaDbContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<RoomBooking> RoomBookings { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourBooking> TourBookings { get; set; }
        public DbSet<RoomCategory> RoomCategories { get; set; }
        public DbSet<CategoryRoomImage> CategoryRoomImages { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
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

            // 1. Seed Data cho Room (Bổ sung RatingStars) 
            modelBuilder.Entity<RoomCategory>().HasData(
                new RoomCategory { Id = 1, Name = "Wooden House", BannerUrl = "..." },
                new RoomCategory { Id = 2, Name = "Khu Villa", BannerUrl = "..." },
                new RoomCategory { Id = 3, Name = "Rose House", BannerUrl = "..." }
            );
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
                    HasTowel = true,
                    RoomCategoryId = 2
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
                    HasTowel = true,
                    RoomCategoryId = 1
                }
            );

            // 2. Seed Data cho Booking (Fix lỗi CustomerPhone, PaymentMethod, v.v.) [cite: 14, 21]
            modelBuilder.Entity<RoomBooking>().HasData(
                new RoomBooking
                {
                    Id = 1,
                    RoomId = 1,
                    CustomerId = null,
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
                    Id = 1,
                    Title = "Tổ chức sinh nhật",
                    Description = "Chúng tôi cung cấp không gian tổ chức sinh nhật ấm cúng, trang trí theo yêu cầu với phong cách hiện đại hoặc cổ điển. Dịch vụ trọn gói bao gồm tiệc trà, bánh ngọt.",
                    EventDate = DateTime.Now.AddDays(7), // Diễn ra sau 7 ngày
                    Location = "Sân vườn Villa",
                    TotalTickets = 50,
                    ImageUrl = "/images/events/birthday.png"
                },
                new Event
                {
                    Id = 2,
                    Title = "Teambuilding",
                    Description = "Không gian sân vườn rộng rãi thích hợp cho các hoạt động ngoài trời, gắn kết tinh thần đồng đội. Hỗ trợ setup các trò chơi vận động, âm thanh, ánh sáng.",
                    EventDate = DateTime.Now.AddDays(14),
                    Location = "Bãi cỏ trung tâm",
                    TotalTickets = 200,
                    ImageUrl = "/images/events/teambuilding.png"
                },
                new Event
                {
                    Id = 3,
                    Title = "Tiệc cưới nhỏ",
                    Description = "Một lễ cưới thân mật, lãng mạn bên những người thân yêu nhất. Không gian được trang hoàng lộng lẫy với hoa tươi, nến và phong cách phục vụ chuẩn 5 sao.",
                    EventDate = DateTime.Now.AddDays(30),
                    Location = "Sảnh tiệc chính",
                    TotalTickets = 100,
                    ImageUrl = "/images/events/wedding.png"
                },
                new Event
                {
                    Id = 4,
                    Title = "Lễ kỷ niệm",
                    Description = "Kỷ niệm ngày cưới, gặp mặt bạn cũ hay những cột mốc quan trọng. Chúng tôi mang đến không gian riêng tư, thực đơn phong phú và sự phục vụ tận tâm.",
                    EventDate = DateTime.Now.AddDays(10),
                    Location = "Nhà hàng ven hồ",
                    TotalTickets = 30,
                    ImageUrl = "/images/events/celebrate.png"
                }
            );
            // -----------------------------------------------------------
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
                new Tour { Id = 1, TourName = "Trekking Ba Vì", Description = "Khám phá vẻ đẹp núi rừng.", PricePerPerson = 500000, DurationHours = 6, ImageUrl = "/images/tours/trekking-bavi.jpg" }
            );

            modelBuilder.Entity<News>().HasData(
                new News { Id = 1, Title = "Music Concert Night", Category = "Sự kiện âm nhạc", ImageUrl = "/images/news/news1.png" },
                new News { Id = 2, Title = "New Villa Opening", Category = "Tin tức", ImageUrl = "/images/news/news2.jpg" }
            );
        }
    }
}