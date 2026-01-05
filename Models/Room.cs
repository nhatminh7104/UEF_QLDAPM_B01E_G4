using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VillaManagementWeb.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = null!;
        public string Type { get; set; } = null!; // Villa, Bungalow, Suite
        [Display(Name = "Danh mục phòng")]
        public int RoomCategoryId { get; set; } // Khóa ngoại

        [ForeignKey("RoomCategoryId")]
        public RoomCategory? RoomCategory { get; set; } // Navigation Property
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public int RatingStars { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Description { get; set; }
        public int Bedrooms { get; set; }          // Số phòng ngủ (VD: 1, 2)
        public int Beds { get; set; }              // Số giường (VD: 1, 2, 3)
        public int CapacityChildren { get; set; }  // Số trẻ em tối đa
        public string? ImageUrl { get; set; }    
        public double? SquareFootage { get; set; }
        public bool HasWifi { get; set; } = true;
        public bool HasBreakfast { get; set; } = true;
        public bool HasPool { get; set; } = true;
        public bool HasTowel { get; set; } = true;

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
