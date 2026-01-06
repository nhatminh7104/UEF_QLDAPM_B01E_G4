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
}