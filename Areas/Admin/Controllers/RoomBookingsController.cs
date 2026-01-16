using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Services.Interfaces;

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoomBookingsController : Controller
    {
        private readonly IRoomBookingsService _RoomBookingsService;
        private readonly IRoomsService _roomsService;

        public RoomBookingsController(IRoomBookingsService RoomBookingsService, IRoomsService roomsService)
        {
            _RoomBookingsService = RoomBookingsService;
            _roomsService = roomsService;
        }

        public async Task<IActionResult> Index() => View(await _RoomBookingsService.GetRoomBookingsWithRoomsAsync());

        public async Task<IActionResult> Details(int id)
        {
            var RoomBooking = await _RoomBookingsService.GetRoomBookingByIdAsync(id);
            return RoomBooking == null ? NotFound() : View(RoomBooking);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.RoomId = new SelectList(await _roomsService.GetRoomsWithStatusAsync(), "Id", "RoomNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomBooking RoomBooking)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _RoomBookingsService.CreateRoomBookingAsync(RoomBooking);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewBag.RoomId = new SelectList(await _roomsService.GetRoomsWithStatusAsync(), "Id", "RoomNumber", RoomBooking.RoomId);
            return View(RoomBooking);
        }

        // Các phương thức Edit và Delete gọi tương tự qua _RoomBookingsService...
    }
}
