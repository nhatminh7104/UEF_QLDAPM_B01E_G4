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

        public RoomImagesController(IRoomImagesService imageService, IRoomsService roomService)
        {
            _imageService = imageService;
            _roomService = roomService;
        }

        // GET: Admin/RoomImages
        public async Task<IActionResult> Index()
        {
            return View(await _imageService.GetAllImagesAsync());
        }

        // GET: Admin/RoomImages/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var roomImage = await _imageService.GetImageByIdAsync(id);
            if (roomImage == null) return NotFound();
            return View(roomImage);
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
        public async Task<IActionResult> Create(RoomImage roomImage)
        {
            if (ModelState.IsValid)
            {
                await _imageService.CreateImageAsync(roomImage);
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, RoomImage roomImage)
        {
            if (id != roomImage.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _imageService.UpdateImageAsync(roomImage);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Lỗi cập nhật ảnh.");
                }
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
            await _imageService.DeleteImageAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}