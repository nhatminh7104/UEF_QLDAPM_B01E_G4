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

            // 1. Lấy Category Info
            var categoryEntry = await _context.RoomCategories
                                              .AsNoTracking()
                                              .FirstOrDefaultAsync(c => c.Name == categoryName);

            if (categoryEntry == null) return View("NotFound");

            // 2. Lấy Rooms và INCLUDE luôn RoomImages
            var dbRooms = await _context.Rooms
                                        .AsNoTracking()
                                        .Include(r => r.RoomImages) // [Quan Trọng] Phải Include bảng này
                                        .Where(r => r.RoomCategoryId == categoryEntry.Id)
                                        .ToListAsync();

            // --- ĐOẠN CODE MỚI ĐỂ LẤY GALLERY ---
            // Gom tất cả ảnh từ các phòng con, lấy tối đa 3-6 ảnh để hiển thị Gallery
            var galleryUrls = dbRooms.SelectMany(r => r.RoomImages)
                                     .Select(img => img.ImageUrl)
                                     .Take(3) // Lấy 3 ảnh đầu tiên tìm thấy
                                     .ToList();

            // Nếu không có ảnh nào trong RoomImages, có thể fallback về ảnh đại diện phòng
            if (!galleryUrls.Any())
            {
                galleryUrls = dbRooms.Where(r => !string.IsNullOrEmpty(r.ImageUrl))
                                     .Select(r => r.ImageUrl)
                                     .Take(3)
                                     .ToList();
            }
            // ------------------------------------

            // 3. Map dữ liệu sang ViewModel
            var model = new RoomCategoryViewModel
            {
                CategoryName = categoryEntry.Name,
                Description = categoryEntry.Description ?? "Đang cập nhật...",
                BannerImage = categoryEntry.BannerUrl ?? "/images/default-banner.jpg",

                // Gán danh sách ảnh vừa lấy được vào đây
                GalleryImages = galleryUrls,

                Amenities = !string.IsNullOrEmpty(categoryEntry.Amenities)
                            ? categoryEntry.Amenities.Split('\n').ToList()
                            : new List<string>(),

                Rooms = dbRooms.Select(r => new RoomItemViewModel
                {
                    Id = r.Id,
                    Name = r.Type ?? r.RoomNumber,
                    Description = r.Description,
                    // Logic lấy ảnh đại diện: Nếu Room có ImageUrl thì lấy, ko thì lấy ảnh đầu trong Album
                    ImageUrl = !string.IsNullOrEmpty(r.ImageUrl)
                               ? r.ImageUrl
                               : (r.RoomImages.FirstOrDefault()?.ImageUrl ?? "/images/no-image.png"),

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