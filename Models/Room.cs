namespace VillaManagementWeb.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = null!;
        public string Type { get; set; } = null!; // Villa, Bungalow, Suite
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public int RatingStars { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }    
        public double? SquareFootage { get; set; }

        // Quan hệ 1-N với Booking, RoomImages
        public ICollection<Booking> Bookings { get; set; } = null!;
        public ICollection<RoomImage> RoomImages { get; set; } = new List<RoomImage>();
        public Room()
        {
            RoomImages = new List<RoomImage>(); 
            Bookings = new List<Booking>();
        }
    }
}
