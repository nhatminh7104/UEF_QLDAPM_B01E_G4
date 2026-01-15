using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Services.Interfaces;

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize(Roles = "Admin")]
    public class TourBookingsController : Controller
    {
        private readonly ITourBookingsService _bookingService;
        private readonly IToursService _tourService;

        public TourBookingsController(
            ITourBookingsService bookingService,
            IToursService tourService)
        {
            _bookingService = bookingService;
            _tourService = tourService;
        }

        // GET: Admin/TourBookings
        public async Task<IActionResult> Index()
        {
            return View(await _bookingService.GetAllBookingsAsync());
        }

        // GET: Admin/TourBookings/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();

            return View(booking);
        }

        // GET: Admin/TourBookings/Create
        public async Task<IActionResult> Create()
        {
            ViewData["TourId"] = new SelectList(
                await _tourService.GetAllToursAsync(),
                "Id",
                "Title");

            return View();
        }

        // POST: Admin/TourBookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TourBooking tourBooking)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _bookingService.CreateBookingAsync(tourBooking);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewData["TourId"] = new SelectList(
                await _tourService.GetAllToursAsync(),
                "Id",
                "Title",
                tourBooking.TourId);

            return View(tourBooking);
        }

        // GET: Admin/TourBookings/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();

            ViewData["TourId"] = new SelectList(
                await _tourService.GetAllToursAsync(),
                "Id",
                "Title",
                booking.TourId);

            return View(booking);
        }

        // POST: Admin/TourBookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TourBooking tourBooking)
        {
            if (id != tourBooking.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _bookingService.UpdateBookingAsync(tourBooking);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewData["TourId"] = new SelectList(
                await _tourService.GetAllToursAsync(),
                "Id",
                "Title",
                tourBooking.TourId);

            return View(tourBooking);
        }

        // GET: Admin/TourBookings/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();

            return View(booking);
        }

        // POST: Admin/TourBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookingService.DeleteBookingAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
