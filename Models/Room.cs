namespace VillaManagementWeb.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string Type { get; set; } // Villa, Bungalow, Suite
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public bool IsActive { get; set; } = true;

        // Quan hệ 1-N với Booking
        public ICollection<Booking> Bookings { get; set; }
    }
}
