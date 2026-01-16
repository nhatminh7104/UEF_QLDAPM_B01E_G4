using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Services.Interfaces;

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoomsController : Controller
    {
        private readonly IRoomsService _roomsService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public RoomsController(IRoomsService roomsService, IWebHostEnvironment hostEnvironment)
        {
            _roomsService = roomsService;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Admin/Rooms
        public async Task<IActionResult> Index()
        {
            var rooms = await _roomsService.GetRoomsWithStatusAsync();
            return View(rooms);
        }

        // GET: Admin/Rooms/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var room = await _roomsService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();
            return View(room);
        }

        // GET: Admin/Rooms/Create
        public async Task<IActionResult> Create()
        {
            ViewData["RoomCategoryId"] =
                new SelectList(await _roomsService.GetRoomCategoriesAsync(), "Id", "Name");
            return View();
        }

        // POST: Admin/Rooms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room, List<IFormFile> imageFiles)
        {
            if (ModelState.IsValid)
            {
                var success = await _roomsService.CreateRoomAsync(room, imageFiles);
                if (success) return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "Có lỗi xảy ra khi tạo phòng.");
            }

            ViewData["RoomCategoryId"] =
                new SelectList(await _roomsService.GetRoomCategoriesAsync(), "Id", "Name", room.RoomCategoryId);

            return View(room);
        }

        // GET: Admin/Rooms/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var room = await _roomsService.GetRoomWithImagesAsync(id);
            if (room == null) return NotFound();

            ViewData["RoomCategoryId"] =
                new SelectList(await _roomsService.GetRoomCategoriesAsync(), "Id", "Name", room.RoomCategoryId);

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
                var success = await _roomsService.UpdateRoomAsync(room, imageFiles);
                if (success) return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "Không thể cập nhật phòng.");
            }

            ViewData["RoomCategoryId"] =
                new SelectList(await _roomsService.GetRoomCategoriesAsync(), "Id", "Name", room.RoomCategoryId);

            return View(room);
        }

        // POST: Admin/Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _roomsService.DeleteRoomAsync(id);
            if (!success) return BadRequest("Không thể xóa phòng.");

            return RedirectToAction(nameof(Index));
        }

        // AJAX: Xóa ảnh lẻ trong album
        [HttpPost]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            var success = await _roomsService.DeleteRoomImageAsync(imageId);
            if (!success)
                return Json(new { success = false, message = "Không tìm thấy ảnh" });

            return Json(new { success = true });
        }

        // Upload ảnh cho Summernote
        [HttpPost]
        public async Task<IActionResult> SummernoteUploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest();

            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string folder = Path.Combine(_hostEnvironment.WebRootPath, "uploads", "summernote");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string path = Path.Combine(folder, fileName);
            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return Json(new { url = "/uploads/summernote/" + fileName });
        }
    }
}
