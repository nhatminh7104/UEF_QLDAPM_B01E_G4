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

        public async Task<IActionResult> CategoryDetails(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName)) return RedirectToAction("Index", "Home");

            // 1. Lấy Category Info VÀ Include bảng CategoryRoomImages
            var categoryEntry = await _context.RoomCategories
                                              .AsNoTracking()
                                              .Include(c => c.CategoryImages) // [MỚI] Lấy ảnh của Category
                                              .FirstOrDefaultAsync(c => c.Name == categoryName);

            if (categoryEntry == null) return View("NotFound");

            // 2. Lấy Rooms (để hiển thị danh sách bên dưới)
            var dbRooms = await _context.Rooms
                                        .AsNoTracking()
                                        .Include(r => r.RoomImages)
                                        .Where(r => r.RoomCategoryId == categoryEntry.Id)
                                        .ToListAsync();

            // 3. Map dữ liệu sang ViewModel
            var model = new RoomCategoryViewModel
            {
                CategoryName = categoryEntry.Name,
                Description = categoryEntry.Description ?? "Đang cập nhật...",
                BannerImage = categoryEntry.BannerUrl ?? "/images/default-banner.jpg",

                // [MỚI] Lấy ảnh từ bảng CategoryRoomImage
                GalleryImages = categoryEntry.CategoryImages
                                             .Select(img => img.ImageUrl)
                                             .Take(3) // Lấy 3 ảnh
                                             .ToList(),

                Amenities = !string.IsNullOrEmpty(categoryEntry.Amenities)
                            ? categoryEntry.Amenities.Split('\n').ToList()
                            : new List<string>(),

                Rooms = dbRooms.Select(r => new RoomItemViewModel
                {
                    Id = r.Id,
                    Name = r.Type ?? r.RoomNumber,
                    Description = r.Description,
                    // Lấy ảnh đại diện phòng
                    ImageUrl = !string.IsNullOrEmpty(r.ImageUrl)
                               ? r.ImageUrl
                               : (r.RoomImages.FirstOrDefault()?.ImageUrl ?? "/images/no-image.png"),
                    ImageList = r.RoomImages.Select(img => img.ImageUrl).ToList(),
                    Price = r.PricePerNight,
                    CapacityAdults = r.Capacity,
                    CapacityChildren = r.CapacityChildren,
                    Area = (int)(r.SquareFootage ?? 0),
                    Bedrooms = r.Bedrooms,
                    Beds = r.Beds,
                    DetailUrl = $"/Rooms/Detail/{r.Id}"
                }).ToList()
            };

            return View("CategoryDetails", model);
        }
    }
}