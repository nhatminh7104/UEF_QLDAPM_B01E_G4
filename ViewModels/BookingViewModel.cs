using System.ComponentModel.DataAnnotations;
using VillaManagementWeb.Models;

namespace VillaManagementWeb.ViewModels
{
    public class BookingPageViewModel
    {
        // Dùng để hiển thị danh sách phòng kèm trạng thái availability
        public List<RoomBookingItemVM> Rooms { get; set; } = new List<RoomBookingItemVM>();
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }

    public class RoomBookingItemVM
    {
        public int RoomId { get; set; }
        public string Name { get; set; }      // Tên phòng
        public string ImageUrl { get; set; }  // Ảnh đại diện
        public decimal Price { get; set; }    // Giá
        public int CapacityAdults { get; set; }
        public int AvailableCount { get; set; } // Số lượng phòng trống
        public int Area { get; set; }
        public int Bedrooms { get; set; }
        public int Beds { get; set; }
    }
    public class TourCheckoutVM
    {
        // Thông tin hiển thị (Read-only)
        public int TourId { get; set; }
        public string TourName { get; set; }
        public string ImageUrl { get; set; }
        public decimal PricePerPerson { get; set; }
        public double Duration { get; set; }

        // Input của khách
        public DateTime TourDate { get; set; } = DateTime.Now.AddDays(1);
        [Range(1, 20)]
        public int NumberOfPeople { get; set; } = 1;
        public decimal TotalAmount { get; set; } // Tạm tính để hiện thị

        // Form điền thông tin
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string CustomerPhone { get; set; }
        [EmailAddress]
        public string? CustomerEmail { get; set; }
        public string? Note { get; set; }
    }

    // 2. Dùng cho trang Mua vé Event
    public class TicketCheckoutVM
    {
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public string ImageUrl { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }

        [Range(1, 10)]
        public int Quantity { get; set; } = 1;
        public string TicketType { get; set; } = "Standard"; // Standard / VIP
        public decimal UnitPrice { get; set; } // Giá đơn vị

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string CustomerPhone { get; set; }
    }
}