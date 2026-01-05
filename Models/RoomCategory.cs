using System.ComponentModel.DataAnnotations;

namespace VillaManagementWeb.Models
{
    public class RoomCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!; // VD: "Wooden House", "Khu Villa" (Phải khớp với chuỗi trong bảng Room)

        [Display(Name = "Ảnh Banner")]
        public string? BannerUrl { get; set; }

        [Display(Name = "Mô tả ngắn")]
        public string? ShortDescription { get; set; } // Đoạn text nhỏ dưới tiêu đề

        [Display(Name = "Bài viết giới thiệu")]
        public string? Description { get; set; } // Nội dung dài (HTML/Summernote)

        [Display(Name = "Danh sách tiện ích")]
        public string? Amenities { get; set; } // Lưu dạng text, mỗi dòng là 1 tiện ích (để split \n như view hiện tại)
        public ICollection<Room> Rooms { get; set; }
    }
}