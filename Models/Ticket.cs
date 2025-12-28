namespace VillaManagementWeb.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public string TicketType { get; set; } = null!; // VIP, Standard
        public decimal Price { get; set; }
        public string CustomerEmail { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public int Quantity { get; set; }
        public DateTime BookingDate { get; set; }
        public string QRCode { get; set; } = null!; // Chuỗi để gen mã QR
        public bool IsUsed { get; set; } = false;
    }
}
