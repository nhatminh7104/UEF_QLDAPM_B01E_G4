using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VillaManagementWeb.Models
{
    public class RoomImage
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!; // Đường dẫn ảnh 

        // Khóa ngoại liên kết tới bảng Room
        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; } = null!; // Navigation property 
    }
}
