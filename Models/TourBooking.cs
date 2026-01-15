using System.ComponentModel.DataAnnotations.Schema;

namespace VillaManagementWeb.Models
{
    public class TourBooking
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; } = null!;

        public DateTime TourDate { get; set; }
        public int NumberOfPeople { get; set; }
        public decimal TotalPrice { get; set; }
        public int? CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }

        // --- CÁC TRƯỜNG CŨ (Giữ lại nếu muốn tương thích ngược, hoặc có thể xóa nếu không dùng) ---
        public string? ContactInfo { get; set; }

        // --- CÁC TRƯỜNG MỚI CẦN THÊM (Fix lỗi CS0117) ---
        public string CustomerName { get; set; } = null!;
        public string? CustomerPhone { get; set; } // Thêm dòng này
        public string? CustomerEmail { get; set; } // Thêm dòng này
        public string? Note { get; set; }          // Thêm dòng này
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Thêm dòng này

        public string Status { get; set; } = "Pending";
    }
}