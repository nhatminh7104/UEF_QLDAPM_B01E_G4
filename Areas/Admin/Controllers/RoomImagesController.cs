using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoomImagesController : Controller
    {
        private readonly VillaDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment; // Cần cái này để xử lý file

        public RoomImagesController(VillaDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Admin/RoomImages
        public async Task<IActionResult> Index()
        {
            var images = await _context.RoomImage.Include(r => r.Room).ToListAsync();
            return View(images);
        }

        // GET: Admin/RoomImages/Create
        public IActionResult Create()
        {
            // Hiển thị tên phòng thay vì chỉ ID
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "RoomNumber");
            return View();
        }

        // POST: Admin/RoomImages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomImage roomImage, IFormFile? imageFile)
        {
            // Bỏ qua validate ImageUrl vì ta sẽ gán nó sau khi upload
            ModelState.Remove("ImageUrl");
            ModelState.Remove("Room");

            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    // Logic upload ảnh
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    string path = Path.Combine(wwwRootPath, "images", "rooms");

                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    roomImage.ImageUrl = "/images/rooms/" + fileName;

                    _context.Add(roomImage);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("ImageUrl", "Vui lòng chọn file ảnh.");
                }
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "RoomNumber", roomImage.RoomId);
            return View(roomImage);
        }

        // GET: Admin/RoomImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var roomImage = await _context.RoomImage.FindAsync(id);
            if (roomImage == null) return NotFound();

            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "RoomNumber", roomImage.RoomId);
            return View(roomImage);
        }

        // POST: Admin/RoomImages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoomImage roomImage, IFormFile? imageFile)
        {
            if (id != roomImage.Id) return NotFound();

            ModelState.Remove("Room");
            ModelState.Remove("ImageUrl"); // Có thể giữ ảnh cũ

            if (ModelState.IsValid)
            {
                try
                {
                    var imageInDb = await _context.RoomImage.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                    if (imageInDb == null) return NotFound();

                    // Nếu có upload ảnh mới -> thay thế
                    if (imageFile != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        string path = Path.Combine(wwwRootPath, "images", "rooms");

                        // Xóa ảnh cũ nếu có
                        if (!string.IsNullOrEmpty(imageInDb.ImageUrl))
                        {
                            var oldPath = Path.Combine(wwwRootPath, imageInDb.ImageUrl.TrimStart('/'));
                            if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                        }

                        using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }
                        roomImage.ImageUrl = "/images/rooms/" + fileName;
                    }
                    else
                    {
                        // Giữ nguyên ảnh cũ
                        roomImage.ImageUrl = imageInDb.ImageUrl;
                    }

                    _context.Update(roomImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.RoomImage.Any(e => e.Id == roomImage.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "RoomNumber", roomImage.RoomId);
            return View(roomImage);
        }

        // GET: Admin/RoomImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var roomImage = await _context.RoomImage.Include(r => r.Room).FirstOrDefaultAsync(m => m.Id == id);
            if (roomImage == null) return NotFound();
            return View(roomImage);
        }

        // POST: Admin/RoomImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomImage = await _context.RoomImage.FindAsync(id);
            if (roomImage != null)
            {
                // Xóa file vật lý
                if (!string.IsNullOrEmpty(roomImage.ImageUrl))
                {
                    var oldPath = Path.Combine(_hostEnvironment.WebRootPath, roomImage.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                }

                _context.RoomImage.Remove(roomImage);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}