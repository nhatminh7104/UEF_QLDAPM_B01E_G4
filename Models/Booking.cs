namespace VillaManagementWeb.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        
        public Room Room { get; set; } = null!;

        public string CustomerName { get; set; } = null!;
        public string CustomerPhone { get; set; } = null!;
        public int AdultsCount { get; set; }
        public int ChildrenCount { get; set; }
        public string? CustomerEmail { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!; // Pending, Confirmed, Cancelled
        public string PaymentMethod { get; set; } = null!; // cash, credit card, online
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
