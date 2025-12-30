using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;

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

        // GET: Controllers/Rooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rooms.ToListAsync());
        }

        // GET: Controllers/Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Controllers/Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Controllers/Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room, List<IFormFile> imageFiles)
        {
            List<string> uploadedFiles = new List<string>(); // Track uploaded files for cleanup on failure

            try
            {
                // Validate ModelState
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại các trường bắt buộc.");
                    return View(room);
                }

                // Validate WebRootPath
                if (string.IsNullOrWhiteSpace(_hostEnvironment?.WebRootPath))
                {
                    ModelState.AddModelError("", "Lỗi hệ thống: Không tìm thấy thư mục WebRoot.");
                    return View(room);
                }

                string wwwRootPath = _hostEnvironment.WebRootPath;
                string roomPath = Path.Combine(wwwRootPath, "images", "rooms");

                // Check and create directory with permission check
                try
                {
                    if (!Directory.Exists(roomPath))
                    {
                        Directory.CreateDirectory(roomPath);
                    }

                    // Check write permissions
                    string testFile = Path.Combine(roomPath, ".write_test");
                    try
                    {
                        System.IO.File.WriteAllText(testFile, "test");
                        System.IO.File.Delete(testFile);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        ModelState.AddModelError("", "Lỗi quyền truy cập: Ứng dụng không có quyền ghi vào thư mục images/rooms. Vui lòng kiểm tra quyền truy cập thư mục.");
                        return View(room);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Lỗi kiểm tra quyền ghi file: {ex.Message}");
                        return View(room);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi tạo thư mục: {ex.Message}");
                    return View(room);
                }

                // Initialize RoomImages collection if null
                if (room.RoomImages == null)
                {
                    room.RoomImages = new List<RoomImage>();
                }

                // Process image files with detailed error handling
                if (imageFiles != null && imageFiles.Count > 0)
                {
                    const long maxFileSize = 5 * 1024 * 1024; // 5MB
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };

                    for (int i = 0; i < imageFiles.Count; i++)
                    {
                        try
                        {
                            var file = imageFiles[i];

                            // Validate file is not null
                            if (file == null)
                            {
                                ModelState.AddModelError("", $"File ảnh thứ {i + 1} không hợp lệ (null).");
                                CleanupUploadedFiles(uploadedFiles);
                                return View(room);
                            }

                            // Validate file name
                            if (string.IsNullOrWhiteSpace(file.FileName))
                            {
                                ModelState.AddModelError("", $"File ảnh thứ {i + 1} không có tên file.");
                                CleanupUploadedFiles(uploadedFiles);
                                return View(room);
                            }

                            // Validate file size
                            if (file.Length == 0)
                            {
                                ModelState.AddModelError("", $"File ảnh thứ {i + 1} rỗng.");
                                CleanupUploadedFiles(uploadedFiles);
                                return View(room);
                            }

                            if (file.Length > maxFileSize)
                            {
                                ModelState.AddModelError("", $"File ảnh thứ {i + 1} vượt quá kích thước cho phép (5MB).");
                                CleanupUploadedFiles(uploadedFiles);
                                return View(room);
                            }

                            // Validate file extension
                            string? fileExtension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
                            if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
                            {
                                ModelState.AddModelError("", $"File ảnh thứ {i + 1} có định dạng không được hỗ trợ. Chỉ chấp nhận: {string.Join(", ", allowedExtensions)}");
                                CleanupUploadedFiles(uploadedFiles);
                                return View(room);
                            }

                            // Generate unique file name
                            string fileName = Guid.NewGuid().ToString() + fileExtension;
                            string fullPath = Path.Combine(roomPath, fileName);

                            // Save file
                            try
                            {
                                using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
                                {
                                    await file.CopyToAsync(fileStream);
                                }

                                // Track uploaded file for cleanup on failure
                                uploadedFiles.Add(fullPath);

                                string dbPath = "/images/rooms/" + fileName;

                                // Set main image or add to gallery
                                if (i == 0)
                                {
                                    room.ImageUrl = dbPath;
                                }
                                else
                                {
                                    // Create RoomImage with navigation property set
                                    // EF Core will automatically set RoomId when Room is saved
                                    room.RoomImages.Add(new RoomImage 
                                    { 
                                        ImageUrl = dbPath,
                                        Room = room // Set navigation property so EF handles RoomId
                                    });
                                }
                            }
                            catch (UnauthorizedAccessException ex)
                            {
                                ModelState.AddModelError("", $"Lỗi quyền truy cập khi lưu file {file.FileName}: {ex.Message}");
                                CleanupUploadedFiles(uploadedFiles);
                                return View(room);
                            }
                            catch (IOException ex)
                            {
                                ModelState.AddModelError("", $"Lỗi I/O khi lưu file {file.FileName}: {ex.Message}");
                                CleanupUploadedFiles(uploadedFiles);
                                return View(room);
                            }
                            catch (Exception ex)
                            {
                                ModelState.AddModelError("", $"Lỗi khi lưu file {file.FileName}: {ex.Message}");
                                CleanupUploadedFiles(uploadedFiles);
                                return View(room);
                            }
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", $"Lỗi xử lý file ảnh thứ {i + 1}: {ex.Message}");
                            CleanupUploadedFiles(uploadedFiles);
                            return View(room);
                        }
                    }
                }

                // Save Room to database
                // EF Core will automatically handle the RoomId foreign key relationship
                // when we add Room with its RoomImages collection
                try
                {
                    _context.Add(room);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", $"Lỗi cơ sở dữ liệu: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        ModelState.AddModelError("", $"Chi tiết: {ex.InnerException.Message}");
                    }
                    CleanupUploadedFiles(uploadedFiles);
                    return View(room);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi lưu dữ liệu: {ex.Message}");
                    CleanupUploadedFiles(uploadedFiles);
                    return View(room);
                }
            }
            catch (Exception ex)
            {
                // Catch any unexpected errors
                ModelState.AddModelError("", $"Lỗi hệ thống không mong đợi: {ex.Message}");
                if (ex.InnerException != null)
                {
                    ModelState.AddModelError("", $"Chi tiết lỗi: {ex.InnerException.Message}");
                }
                CleanupUploadedFiles(uploadedFiles);
                return View(room);
            }
        }

        /// <summary>
        /// Clean up uploaded files if operation fails
        /// </summary>
        private void CleanupUploadedFiles(List<string> filePaths)
        {
            if (filePaths == null || filePaths.Count == 0) return;

            foreach (var filePath in filePaths)
            {
                try
                {
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                catch
                {
                    // Silently ignore cleanup errors to avoid masking the original error
                }
            }
        }

        // GET: Controllers/Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        // POST: Controllers/Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomNumber,Type,PricePerNight,Capacity,RatingStars,IsActive,Description,ImageUrl,SquareFootage")] Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Controllers/Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Controllers/Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> SummernoteUploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest();

            // Tạo đường dẫn lưu file
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var path = Path.Combine(_hostEnvironment.WebRootPath, "uploads", "summernote", fileName);

            // Đảm bảo thư mục tồn tại
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Trả về URL của ảnh để Summernote chèn vào nội dung
            return Json(new { url = "/uploads/summernote/" + fileName });
        }
        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
    }
}
