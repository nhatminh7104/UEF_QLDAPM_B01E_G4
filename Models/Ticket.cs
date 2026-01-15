using System.ComponentModel.DataAnnotations.Schema;

namespace VillaManagementWeb.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public string TicketType { get; set; } = null!;
        public decimal Price { get; set; }
        public int? CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }

        public string CustomerEmail { get; set; } = null!;
        public string CustomerName { get; set; } = null!;

        // --- TRƯỜNG MỚI CẦN THÊM (Fix lỗi CS0117) ---
        public string? CustomerPhone { get; set; } // Thêm dòng này

        public int Quantity { get; set; }
        public DateTime BookingDate { get; set; } // Model dùng BookingDate
        public string QRCode { get; set; } = null!;
        public bool IsUsed { get; set; } = false;
    }
}