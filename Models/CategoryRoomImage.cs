using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VillaManagementWeb.Models
{
    public class CategoryRoomImage
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!; // Đường dẫn ảnh

        // Khóa ngoại liên kết tới bảng RoomCategory
        public int RoomCategoryId { get; set; }

        [ForeignKey("RoomCategoryId")]
        public RoomCategory RoomCategory { get; set; } = null!;
    }
}