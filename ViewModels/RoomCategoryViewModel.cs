using System.Collections.Generic;

namespace VillaManagementWeb.ViewModels
{
    public class RoomCategoryViewModel
    {
        public string CategoryName { get; set; } // Ví dụ: Wooden House, Rose House
        public string Description { get; set; }  // Phần "Giới thiệu"
        public string BannerImage { get; set; }  // Ảnh lớn đầu trang
        public List<string> GalleryImages { get; set; } // 3 ảnh nhỏ phần giới thiệu
        public List<RoomItemViewModel> Rooms { get; set; } // Danh sách các hạng phòng
        public List<string> Amenities { get; set; } // Danh sách tiện nghi chung
    }

    public class RoomItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } // Forest room, Deluxe room...
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<string> ImageList { get; set; } = new List<string>(); // Danh sách ảnh slide
        public decimal Price { get; set; }
        public int CapacityAdults { get; set; }
        public int CapacityChildren { get; set; }
        public int Area { get; set; } // m2
        public int Bedrooms { get; set; }
        public int Beds { get; set; }
        public string DetailUrl { get; set; }
    }
}