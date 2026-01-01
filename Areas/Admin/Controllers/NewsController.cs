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
using VillaManagementWeb.Services.Interfaces;

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public NewsController(INewsService newsService, IWebHostEnvironment hostEnvironment)
        {
            _newsService = newsService;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Admin/News
        public async Task<IActionResult> Index()
        {
            return View(await _newsService.GetAllNewsAsync());
        }

        // GET: Admin/News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _newsService.GetNewsByIdAsync(id.Value);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: Admin/News/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(News news, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Handle image upload
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        // Validate file
                        const long maxFileSize = 5 * 1024 * 1024; // 5MB
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
                        
                        string? fileExtension = Path.GetExtension(imageFile.FileName)?.ToLowerInvariant();
                        if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
                        {
                            ModelState.AddModelError("", $"File ảnh có định dạng không được hỗ trợ. Chỉ chấp nhận: {string.Join(", ", allowedExtensions)}");
                            return View(news);
                        }

                        if (imageFile.Length > maxFileSize)
                        {
                            ModelState.AddModelError("", "File ảnh vượt quá kích thước cho phép (5MB).");
                            return View(news);
                        }

                        // Save file
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string newsPath = Path.Combine(wwwRootPath, "images", "news");
                        
                        if (!Directory.Exists(newsPath))
                        {
                            Directory.CreateDirectory(newsPath);
                        }

                        string fileName = Guid.NewGuid().ToString() + fileExtension;
                        string fullPath = Path.Combine(newsPath, fileName);

                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        news.ImageUrl = "/images/news/" + fileName;
                    }

                    await _newsService.CreateNewsAsync(news);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(news);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi tạo tin tức: {ex.Message}");
                    return View(news);
                }
            }
            return View(news);
        }

        // GET: Admin/News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _newsService.GetNewsByIdAsync(id.Value);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, News news, IFormFile imageFile)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Get existing news to preserve ImageUrl if no new file is uploaded
                    var existingNews = await _newsService.GetNewsByIdAsync(id);
                    if (existingNews == null)
                    {
                        return NotFound();
                    }

                    // Handle image upload
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        // Validate file
                        const long maxFileSize = 5 * 1024 * 1024; // 5MB
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
                        
                        string? fileExtension = Path.GetExtension(imageFile.FileName)?.ToLowerInvariant();
                        if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
                        {
                            ModelState.AddModelError("", $"File ảnh có định dạng không được hỗ trợ. Chỉ chấp nhận: {string.Join(", ", allowedExtensions)}");
                            return View(news);
                        }

                        if (imageFile.Length > maxFileSize)
                        {
                            ModelState.AddModelError("", "File ảnh vượt quá kích thước cho phép (5MB).");
                            return View(news);
                        }

                        // Save file
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string newsPath = Path.Combine(wwwRootPath, "images", "news");
                        
                        if (!Directory.Exists(newsPath))
                        {
                            Directory.CreateDirectory(newsPath);
                        }

                        string fileName = Guid.NewGuid().ToString() + fileExtension;
                        string fullPath = Path.Combine(newsPath, fileName);

                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        news.ImageUrl = "/images/news/" + fileName;
                    }
                    else
                    {
                        // Keep existing image if no new file is uploaded
                        news.ImageUrl = existingNews.ImageUrl;
                    }

                    // Preserve CreatedAt from existing news
                    news.CreatedAt = existingNews.CreatedAt;

                    await _newsService.UpdateNewsAsync(news);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(news);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi cập nhật tin tức: {ex.Message}");
                    return View(news);
                }
            }
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _newsService.GetNewsByIdAsync(id.Value);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _newsService.DeleteNewsAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

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
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Trả về URL của ảnh để Summernote chèn vào nội dung
            return Json(new { url = "/uploads/summernote/" + fileName });
        }
    }
}
