using System.ComponentModel.DataAnnotations;

namespace VillaManagementWeb.Models
{
    public class RoomCategory
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tên danh mục")]
        [Required(ErrorMessage = "Vui lòng nhập tên danh mục.")]
        [StringLength(100, ErrorMessage = "Tên danh mục không được vượt quá 100 ký tự.")]
        public string Name { get; set; } = null!; // VD: "Wooden House", "Khu Villa" (Phải khớp với chuỗi trong bảng Room)

        [Display(Name = "Ảnh Banner")]
        public string? BannerUrl { get; set; }

        [Display(Name = "Mô tả ngắn")]
        [StringLength(500, ErrorMessage = "Mô tả ngắn không được vượt quá 500 ký tự.")]
        public string? ShortDescription { get; set; } // Đoạn text nhỏ dưới tiêu đề

        [Display(Name = "Bài viết giới thiệu")]
        public string? Description { get; set; } // Nội dung dài (HTML/Summernote)

        [Display(Name = "Danh sách tiện ích")]
        public string? Amenities { get; set; } // Lưu dạng text, mỗi dòng là 1 tiện ích (để split \n như view hiện tại)
        public ICollection<Room> Rooms { get; set; }
    }
}