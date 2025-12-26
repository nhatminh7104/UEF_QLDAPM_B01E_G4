namespace VillaManagementWeb.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }

        public string TicketType { get; set; } // VIP, Standard
        public decimal Price { get; set; }
        public string CustomerEmail { get; set; }
        public string QRCode { get; set; } // Chuỗi để gen mã QR
        public bool IsUsed { get; set; } = false;
    }
}
