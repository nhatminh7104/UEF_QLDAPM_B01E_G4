using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting; // Thêm namespace này để xử lý file
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using VillaManagementWeb.Models;
using VillaManagementWeb.Services.Interfaces;

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ToursController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IWebHostEnvironment _webHostEnvironment; // Inject môi trường để lấy đường dẫn

        public ToursController(ITourService tourService, IWebHostEnvironment webHostEnvironment)
        {
            _tourService = tourService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Tours
        public async Task<IActionResult> Index()
        {
            return View(await _tourService.GetAllToursAsync());
        }

        // GET: Admin/Tours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var tour = await _tourService.GetTourByIdAsync(id.Value);
            if (tour == null) return NotFound();
            return View(tour);
        }

        // GET: Admin/Tours/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Tours/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tour tour, IFormFile? imageFile)
        {
            ModelState.Remove("TourBookings");
            if (ModelState.IsValid)
            {
                // Xử lý upload ảnh
                if (imageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "tours");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    tour.ImageUrl = "/images/tours/" + uniqueFileName;
                }

                await _tourService.CreateTourAsync(tour);
                return RedirectToAction(nameof(Index));
            }
            return View(tour);
        }

        // GET: Admin/Tours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var tour = await _tourService.GetTourByIdAsync(id.Value);
            if (tour == null) return NotFound();
            return View(tour);
        }

        // POST: Admin/Tours/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tour tour, IFormFile? imageFile)
        {

            if (id != tour.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Xử lý upload ảnh mới nếu có
                    if (imageFile != null)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "tours");
                        if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }
                        tour.ImageUrl = "/images/tours/" + uniqueFileName;
                    }

                    await _tourService.UpdateTourAsync(tour);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi: {ex.Message}");
                }
            }
            return View(tour);
        }

        // POST: Admin/Tours/Delete/5 (Dùng cho AJAX)
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _tourService.DeleteTourAsync(id);
                if (deleted)
                {
                    return Json(new { success = true, message = "Xóa tour thành công!" });
                }
                return Json(new { success = false, message = "Không tìm thấy tour hoặc lỗi hệ thống." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}