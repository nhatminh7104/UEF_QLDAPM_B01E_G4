using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.ViewModels; // Cần dùng RoomIndexVM

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoomsController : Controller
    {
        private readonly VillaDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public RoomsController(VillaDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Admin/Rooms
        public async Task<IActionResult> Index()
        {
            // Lấy danh sách từ DB và map sang RoomIndexVM để hiển thị ở View Index
            var rooms = await _context.Rooms
                .Select(r => new RoomIndexVM
                {
                    Id = r.Id,
                    RoomNumber = r.RoomNumber,
                    Type = r.Type,
                    PricePerNight = r.PricePerNight,
                    Capacity = r.Capacity,
                    RatingStars = r.RatingStars,
                    IsActive = r.IsActive,
                    ImageUrl = r.ImageUrl,
                    SquareFootage = r.SquareFootage,
                    HasWifi = r.HasWifi,
                    HasBreakfast = r.HasBreakfast,
                    HasPool = r.HasPool,
                    HasTowel = r.HasTowel,
                    // Giả định trạng thái thuê dựa trên logic nghiệp vụ của bạn
                    StatusLabel = r.IsActive ? "Trống" : "Bảo trì",
                    StatusColor = r.IsActive ? "badge-success" : "badge-danger"
                }).ToListAsync();

            return View(rooms);
        }

        // GET: Admin/Rooms/Create
        public IActionResult Create()
        {
            ViewData["RoomCategoryId"] = new SelectList(_context.RoomCategories, "Id", "Name");
            return View();
        }
        // POST: Admin/Rooms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room, List<IFormFile> imageFiles)
        {
            if (ModelState.IsValid)
            {
                // Xử lý upload nhiều ảnh [cite: 311, 312]
                if (imageFiles != null && imageFiles.Count > 0)
                {
                    string roomPath = Path.Combine(_hostEnvironment.WebRootPath, "images", "rooms");
                    if (!Directory.Exists(roomPath)) Directory.CreateDirectory(roomPath);

                    for (int i = 0; i < imageFiles.Count; i++)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFiles[i].FileName);
                        string dbPath = "/images/rooms/" + fileName;

                        using (var stream = new FileStream(Path.Combine(roomPath, fileName), FileMode.Create))
                        {
                            await imageFiles[i].CopyToAsync(stream);
                        }

                        if (i == 0) room.ImageUrl = dbPath; // Ảnh đầu tiên làm cover [cite: 320]

                        room.RoomImages.Add(new RoomImage { ImageUrl = dbPath });
                    }
                }

                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Admin/Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var room = await _context.Rooms.Include(r => r.RoomImages).FirstOrDefaultAsync(r => r.Id == id);
            if (room == null) return NotFound();
            ViewData["RoomCategoryId"] = new SelectList(_context.RoomCategories, "Id", "Name", room.RoomCategoryId);
            return View(room);
        }

        // POST: Admin/Rooms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Room room, List<IFormFile> imageFiles)
        {
            if (id != room.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingRoom = await _context.Rooms.Include(r => r.RoomImages).FirstOrDefaultAsync(r => r.Id == id);
                    if (existingRoom == null) return NotFound();

                    // Cập nhật các trường mới
                    existingRoom.Bedrooms = room.Bedrooms;
                    existingRoom.Beds = room.Beds;
                    existingRoom.CapacityChildren = room.CapacityChildren;
                    existingRoom.RoomNumber = room.RoomNumber;
                    existingRoom.RoomCategoryId = room.RoomCategoryId;
                    existingRoom.Type = room.Type;
                    existingRoom.PricePerNight = room.PricePerNight;
                    existingRoom.Capacity = room.Capacity;
                    existingRoom.CapacityChildren = room.CapacityChildren;
                    existingRoom.Bedrooms = room.Bedrooms;
                    existingRoom.Beds = room.Beds;
                    existingRoom.SquareFootage = room.SquareFootage;
                    existingRoom.IsActive = room.IsActive;
                    existingRoom.Description = room.Description;
                    existingRoom.HasWifi = room.HasWifi;
                    existingRoom.HasBreakfast = room.HasBreakfast;
                    existingRoom.HasPool = room.HasPool;
                    existingRoom.HasTowel = room.HasTowel;

                    // Upload thêm ảnh mới vào album [cite: 358, 359]
                    if (imageFiles != null && imageFiles.Count > 0)
                    {
                        string roomPath = Path.Combine(_hostEnvironment.WebRootPath, "images", "rooms");
                        foreach (var file in imageFiles)
                        {
                            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                            string dbPath = "/images/rooms/" + fileName;
                            using (var stream = new FileStream(Path.Combine(roomPath, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                            existingRoom.RoomImages.Add(new RoomImage { ImageUrl = dbPath });
                        }
                    }

                    _context.Update(existingRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Rooms.Any(e => e.Id == room.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomCategoryId"] = new SelectList(_context.RoomCategories, "Id", "Name", room.RoomCategoryId);
            return View(room);
        }

        // Action xóa ảnh lẻ trong Album (AJAX) [cite: 394]
        [HttpPost]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            var image = await _context.RoomImage.FindAsync(imageId);
            if (image == null) return Json(new { success = false, message = "Không tìm thấy ảnh" });

            _context.RoomImage.Remove(image);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        // POST: Admin/Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room != null) _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}