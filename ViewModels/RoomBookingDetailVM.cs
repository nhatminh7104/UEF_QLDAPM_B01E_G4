using VillaManagementWeb.Models;

namespace VillaManagementWeb.ViewModels
{
    public class RoomBookingDetailVM
    {
        // 1. Thông tin để hiển thị (Read-only)
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public decimal PricePerNight { get; set; }
        public string Description { get; set; }
        public List<string> Amenities { get; set; }
        public List<string> GalleryImages { get; set; }

        // Thông tin về sức chứa & kho
        public int CapacityAdults { get; set; }
        public int CapacityChildren { get; set; }
        public int MaxAvailableSlots { get; set; } // Số phòng còn trống tối đa của loại này

        // 2. Thông tin khách điền (Form Input)
        public RoomBooking BookingRequest { get; set; } = new RoomBooking();
    }
}