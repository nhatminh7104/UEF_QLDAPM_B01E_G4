using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Services.Interfaces;

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoomImagesController : Controller
    {
        private readonly IRoomImagesService _imageService;
        private readonly IRoomsService _roomService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public RoomImagesController(
            IRoomImagesService imageService,
            IRoomsService roomService,
            IWebHostEnvironment hostEnvironment)
        {
            _imageService = imageService;
            _roomService = roomService;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Admin/RoomImages
        public async Task<IActionResult> Index()
        {
            return View(await _imageService.GetAllImagesAsync());
        }

        // GET: Admin/RoomImages/Create
        public async Task<IActionResult> Create()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            ViewData["RoomId"] = new SelectList(rooms, "Id", "RoomName");
            return View();
        }

        // POST: Admin/RoomImages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomImage roomImage, IFormFile? imageFile)
        {
            ModelState.Remove("ImageUrl");
            ModelState.Remove("Room");

            if (ModelState.IsValid)
            {
                if (imageFile == null)
                {
                    ModelState.AddModelError("ImageUrl", "Vui lòng chọn file ảnh.");
                }
                else
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    string folderPath = Path.Combine(wwwRootPath, "images", "rooms");

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string fullPath = Path.Combine(folderPath, fileName);
                    using var stream = new FileStream(fullPath, FileMode.Create);
                    await imageFile.CopyToAsync(stream);

                    roomImage.ImageUrl = "/images/rooms/" + fileName;

                    await _imageService.CreateImageAsync(roomImage);
                    return RedirectToAction(nameof(Index));
                }
            }

            var rooms = await _roomService.GetAllRoomsAsync();
            ViewData["RoomId"] = new SelectList(rooms, "Id", "RoomName", roomImage.RoomId);
            return View(roomImage);
        }

        // GET: Admin/RoomImages/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var roomImage = await _imageService.GetImageByIdAsync(id);
            if (roomImage == null) return NotFound();

            var rooms = await _roomService.GetAllRoomsAsync();
            ViewData["RoomId"] = new SelectList(rooms, "Id", "RoomName", roomImage.RoomId);
            return View(roomImage);
        }

        // POST: Admin/RoomImages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoomImage roomImage, IFormFile? imageFile)
        {
            if (id != roomImage.Id) return NotFound();

            ModelState.Remove("Room");
            ModelState.Remove("ImageUrl");

            if (ModelState.IsValid)
            {
                var imageInDb = await _imageService.GetImageByIdAsync(id);
                if (imageInDb == null) return NotFound();

                if (imageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;

                    if (!string.IsNullOrEmpty(imageInDb.ImageUrl))
                    {
                        string oldPath = Path.Combine(wwwRootPath, imageInDb.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldPath))
                            System.IO.File.Delete(oldPath);
                    }

                    string fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    string folderPath = Path.Combine(wwwRootPath, "images", "rooms");

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    using var stream = new FileStream(Path.Combine(folderPath, fileName), FileMode.Create);
                    await imageFile.CopyToAsync(stream);

                    roomImage.ImageUrl = "/images/rooms/" + fileName;
                }
                else
                {
                    roomImage.ImageUrl = imageInDb.ImageUrl;
                }

                await _imageService.UpdateImageAsync(roomImage);
                return RedirectToAction(nameof(Index));
            }

            var rooms = await _roomService.GetAllRoomsAsync();
            ViewData["RoomId"] = new SelectList(rooms, "Id", "RoomName", roomImage.RoomId);
            return View(roomImage);
        }

        // GET: Admin/RoomImages/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var roomImage = await _imageService.GetImageByIdAsync(id);
            if (roomImage == null) return NotFound();
            return View(roomImage);
        }

        // POST: Admin/RoomImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _imageService.GetImageByIdAsync(id);
            if (image != null && !string.IsNullOrEmpty(image.ImageUrl))
            {
                string oldPath = Path.Combine(_hostEnvironment.WebRootPath, image.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }

            await _imageService.DeleteImageAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
