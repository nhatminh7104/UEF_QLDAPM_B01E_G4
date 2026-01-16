using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VillaManagementWeb.Models
{
    public class RoomBooking
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn phòng")]
        public int RoomId { get; set; }

        public Room? Room { get; set; }
        public int? CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        [StringLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự")]
        
        public string CustomerName { get; set; } = null!;
        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không đúng định dạng")]
        public string CustomerPhone { get; set; } = null!;
        [Range(1, 20, ErrorMessage = "Số người lớn phải từ 1 đến 20")]
        public int AdultsCount { get; set; }
        public int ChildrenCount { get; set; }
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? CustomerEmail { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ngày Check-in")]
        public DateTime CheckIn { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ngày Check-out")]
        public DateTime CheckOut { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Tổng tiền không được âm")]
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!; // Pending, Confirmed, Cancelled
        [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán")] 
        public string PaymentMethod { get; set; } = null!; // cash, credit card, online
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Display(Name = "Số CCCD/Passport")]
        public string? IdentityCard { get; set; } 

        [Display(Name = "Quốc tịch")]
        public string? Nationality { get; set; }
    }
}
