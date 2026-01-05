using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly VillaDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CategoriesController(VillaDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.RoomCategories
                                   .Include(c => c.Rooms)
                                   .ToListAsync();
            return View(categories);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var category = await _context.RoomCategories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoomCategory category, IFormFile? bannerFile)
        {
            if (id != category.Id) return NotFound();

            if (ModelState.IsValid)
            {
                // Xử lý upload Banner mới
                if (bannerFile != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(bannerFile.FileName);
                    string path = Path.Combine(_hostEnvironment.WebRootPath, "images", "categories");
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        await bannerFile.CopyToAsync(stream);
                    }
                    category.BannerUrl = "/images/categories/" + fileName;
                }
                else
                {
                    // Giữ nguyên ảnh cũ nếu không upload mới (cần logic load lại từ DB để không bị null)
                    var oldCat = await _context.RoomCategories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                    if (oldCat != null) category.BannerUrl = oldCat.BannerUrl;
                }

                _context.Update(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // Bạn có thể thêm Create/Delete tương tự
        public IActionResult Create() => View();
        public IActionResult Delette() => View();

        [HttpPost]
        public async Task<IActionResult> Create(RoomCategory category, IFormFile? bannerFile)
        {
            if (bannerFile != null)
            {
                // (Copy logic upload ảnh ở trên vào đây)
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(bannerFile.FileName);
                string path = Path.Combine(_hostEnvironment.WebRootPath, "images", "categories");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    await bannerFile.CopyToAsync(stream);
                }
                category.BannerUrl = "/images/categories/" + fileName;
            }
            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}