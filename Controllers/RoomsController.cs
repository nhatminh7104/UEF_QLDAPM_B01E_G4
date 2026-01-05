using Microsoft.AspNetCore.Mvc;
using VillaManagementWeb.Data;
using VillaManagementWeb.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace VillaManagementWeb.Controllers
{
    public class RoomsController : Controller
    {
        private readonly VillaDbContext _context;

        public RoomsController(VillaDbContext context)
        {
            _context = context;
        }

        // Action nhận vào tên category (VD: "Wooden House")
        public async Task<IActionResult> CategoryDetails(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName)) return RedirectToAction("Index", "Home");

            // 1. Tìm thông tin Loại phòng trong bảng RoomCategories (Lấy cả Banner, Mô tả)
            // Lưu ý: Dùng AsNoTracking để tối ưu hiệu suất cho trang hiển thị
            var categoryEntry = await _context.RoomCategories
                                              .AsNoTracking()
                                              .FirstOrDefaultAsync(c => c.Name == categoryName);

            if (categoryEntry == null)
            {
                return View("NotFound"); // Hoặc Redirect về Home
            }

            // 2. Lấy danh sách các phòng thuộc loại này (Dùng RoomCategoryId)
            var dbRooms = await _context.Rooms
                                        .AsNoTracking()
                                        .Include(r => r.RoomImages) // Load ảnh
                                        .Where(r => r.RoomCategoryId == categoryEntry.Id) // So sánh theo ID chuẩn khóa ngoại
                                        .ToListAsync();

            // 3. Map dữ liệu sang ViewModel để hiển thị ra View đẹp của bạn
            var model = new RoomCategoryViewModel
            {
                // Lấy dữ liệu động từ bảng RoomCategory
                CategoryName = categoryEntry.Name,
                Description = categoryEntry.Description ?? "Đang cập nhật nội dung...",
                BannerImage = categoryEntry.BannerUrl ?? "/images/default-banner.jpg",

                // Xử lý tiện ích: Tách chuỗi theo dòng (\n) nếu có
                Amenities = !string.IsNullOrEmpty(categoryEntry.Amenities)
                            ? categoryEntry.Amenities.Split('\n').ToList()
                            : new List<string>(),

                // Map danh sách phòng
                Rooms = dbRooms.Select(r => new RoomItemViewModel
                {
                    Name = r.Type ?? r.RoomNumber, // Tên hiển thị (VD: Bungalow)
                    Description = r.Description,

                    // Lấy ảnh đại diện (Ưu tiên ImageUrl, nếu null thì lấy ảnh đầu trong album)
                    ImageUrl = !string.IsNullOrEmpty(r.ImageUrl)
                               ? r.ImageUrl
                               : (r.RoomImages.FirstOrDefault()?.ImageUrl ?? "/images/no-image.png"),

                    Price = r.PricePerNight,
                    CapacityAdults = r.Capacity,
                    CapacityChildren = r.CapacityChildren, // Đã có trường này trong Model mới

                    Area = (int)(r.SquareFootage ?? 0),
                    Bedrooms = r.Bedrooms,
                    Beds = r.Beds,

                    DetailUrl = $"/Rooms/Detail/{r.Id}" // Link chi tiết từng phòng
                }).ToList()
            };

            // Trả về View "CategoryDetails" (File view duy nhất dùng chung)
            return View("CategoryDetails", model);
        }
    }
}