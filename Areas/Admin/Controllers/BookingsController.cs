using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VillaManagementWeb.Admin.Services.Implementations;
using VillaManagementWeb.Admin.Services.Interfaces;
using VillaManagementWeb.Models;
using IRoomsService = VillaManagementWeb.Admin.Services.Interfaces.IRoomsService;

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BookingsController : Controller
    {
        private readonly IRoomBookingsService _bookingService;
        private readonly IRoomsService _roomService;

        public BookingsController(IRoomBookingsService bookingService, RoomsService roomService)
        {
            _bookingService = bookingService;
            _roomService = roomService;
        }

        // GET: Controllers/Bookings
        public async Task<IActionResult> Index()
        {
            return View(await _bookingService.GetRoomBookingsWithRoomsAsync());
        }

        // GET: Controllers/Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _bookingService.GetRoomBookingByIdAsync(id.Value);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Controllers/Bookings/Create
        public async Task<IActionResult> Create()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            ViewData["RoomId"] = new SelectList(rooms, "Id", "RoomNumber");
            return View();
        }

        // POST: Controllers/Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoomId,CustomerName,CustomerPhone,AdultsCount,ChildrenCount,CustomerEmail,CheckIn,CheckOut,TotalAmount,Status,PaymentMethod,Notes,CreatedAt")] RoomBooking booking)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await _bookingService.CreateRoomBookingAsync(booking);
                    TempData["SuccessMessage"] = "Tạo đơn đặt phòng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            var rooms = await _roomService.GetAllRoomsAsync();
            ViewData["RoomId"] = new SelectList(rooms, "Id", "RoomNumber", booking.RoomId);
            return View(booking);
        }

        // GET: Controllers/Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _bookingService.GetRoomBookingByIdAsync(id.Value);
            if (booking == null)
            {
                return NotFound();
            }
            var rooms = await _roomService.GetAllRoomsAsync();
            ViewData["RoomId"] = new SelectList(rooms, "Id", "RoomNumber", booking.RoomId);
            return View(booking);
        }

        // POST: Controllers/Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomId,CustomerName,CustomerPhone,AdultsCount,ChildrenCount,CustomerEmail,CheckIn,CheckOut,TotalAmount,Status,PaymentMethod,Notes,CreatedAt")] RoomBooking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bookingService.UpdateRoomBookingAsync(booking);
                    TempData["SuccessMessage"] = "Cập nhật trạng thái đơn hàng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            var rooms = await _roomService.GetAllRoomsAsync();
            ViewData["RoomId"] = new SelectList(rooms, "Id", "RoomNumber", booking.RoomId);
            return View(booking);
        }

        // GET: Controllers/Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _bookingService.GetRoomBookingByIdAsync(id.Value);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Controllers/Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookingService.DeleteRoomBookingAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
