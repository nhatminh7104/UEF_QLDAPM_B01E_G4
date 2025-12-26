namespace VillaManagementWeb.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }

        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } // Pending, Confirmed, Cancelled
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
