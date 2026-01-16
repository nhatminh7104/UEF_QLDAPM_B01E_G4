using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting; // Cần thêm cái này
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryRoomImagesController : Controller
    {
        private readonly VillaDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment; // Để lấy đường dẫn wwwroot

        public CategoryRoomImagesController(VillaDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/CategoryRoomImages
        public async Task<IActionResult> Index()
        {
            var villaDbContext = _context.CategoryRoomImages.Include(c => c.RoomCategory);
            return View(await villaDbContext.ToListAsync());
        }

        // GET: Admin/CategoryRoomImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var categoryRoomImage = await _context.CategoryRoomImages
                .Include(c => c.RoomCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryRoomImage == null) return NotFound();

            return View(categoryRoomImage);
        }

        // GET: Admin/CategoryRoomImages/Create
        public IActionResult Create()
        {
            ViewData["RoomCategoryId"] = new SelectList(_context.RoomCategories, "Id", "Name");
            return View();
        }

        // POST: Admin/CategoryRoomImages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryRoomImage categoryRoomImage, IFormFile? imageFile)
        {
            ModelState.Remove("ImageUrl");
            ModelState.Remove("RoomCategory");
            if (ModelState.IsValid)
            {
                // Xử lý upload ảnh
                if (imageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "categories");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    categoryRoomImage.ImageUrl = "/images/categories/" + uniqueFileName;
                }
                else
                {
                    // Nếu không up ảnh thì báo lỗi (Vì ImageUrl là Required)
                    ModelState.AddModelError("ImageUrl", "Vui lòng chọn hình ảnh");
                    ViewData["RoomCategoryId"] = new SelectList(_context.RoomCategories, "Id", "Name", categoryRoomImage.RoomCategoryId);
                    return View(categoryRoomImage);
                }

                _context.Add(categoryRoomImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomCategoryId"] = new SelectList(_context.RoomCategories, "Id", "Name", categoryRoomImage.RoomCategoryId);
            return View(categoryRoomImage);
        }

        // GET: Admin/CategoryRoomImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var categoryRoomImage = await _context.CategoryRoomImages.FindAsync(id);
            if (categoryRoomImage == null) return NotFound();

            ViewData["RoomCategoryId"] = new SelectList(_context.RoomCategories, "Id", "Name", categoryRoomImage.RoomCategoryId);
            return View(categoryRoomImage);
        }

        // POST: Admin/CategoryRoomImages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryRoomImage categoryRoomImage, IFormFile? imageFile)
        {
            if (id != categoryRoomImage.Id) return NotFound();
            ModelState.Remove("ImageUrl");
            ModelState.Remove("RoomCategory");
            if (ModelState.IsValid)
            {
                try
                {
                    // Nếu có upload ảnh mới
                    if (imageFile != null)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "categories");
                        if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        // Cập nhật đường dẫn mới
                        categoryRoomImage.ImageUrl = "/images/categories/" + uniqueFileName;
                    }
                    else
                    {
                        // Nếu không chọn ảnh mới -> Giữ nguyên ảnh cũ
                        // Cần query lại DB để lấy ImageUrl cũ nếu form không gửi lên hidden field
                        // Tuy nhiên ở View Edit tôi sẽ thêm hidden field để giữ giá trị này
                    }

                    _context.Update(categoryRoomImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryRoomImageExists(categoryRoomImage.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomCategoryId"] = new SelectList(_context.RoomCategories, "Id", "Name", categoryRoomImage.RoomCategoryId);
            return View(categoryRoomImage);
        }

        // GET: Admin/CategoryRoomImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var categoryRoomImage = await _context.CategoryRoomImages
                .Include(c => c.RoomCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryRoomImage == null) return NotFound();

            return View(categoryRoomImage);
        }

        // POST: Admin/CategoryRoomImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryRoomImage = await _context.CategoryRoomImages.FindAsync(id);
            if (categoryRoomImage != null)
            {
                // Tùy chọn: Xóa file vật lý trên server để dọn rác
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, categoryRoomImage.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                _context.CategoryRoomImages.Remove(categoryRoomImage);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryRoomImageExists(int id)
        {
            return _context.CategoryRoomImages.Any(e => e.Id == id);
        }
    }
}