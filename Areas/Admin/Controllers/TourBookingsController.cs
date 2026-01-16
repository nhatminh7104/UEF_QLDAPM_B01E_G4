using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class TourBookingsController : Controller
    {
        private readonly ITourBookingService _tourBookingService;
        private readonly ITourService _tourService;

        public TourBookingsController(ITourBookingService tourBookingService, ITourService tourService)
        {
            _tourBookingService = tourBookingService;
            _tourService = tourService;
        }

        // GET: Admin/TourBookings
        public async Task<IActionResult> Index(DateTime? tourDate, string? status)
        {
            IEnumerable<TourBooking> tourBookings;

            if (tourDate.HasValue || !string.IsNullOrWhiteSpace(status))
            {
                tourBookings = await _tourBookingService.GetTourBookingsFilteredAsync(tourDate, status);
            }
            else
            {
                tourBookings = await _tourBookingService.GetTourBookingsWithToursAsync();
            }

            // Pass filter values to view for maintaining filter state
            ViewBag.TourDate = tourDate;
            ViewBag.Status = status;

            return View(tourBookings);
        }

        // GET: Admin/TourBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourBooking = await _tourBookingService.GetTourBookingByIdAsync(id.Value);
            if (tourBooking == null)
            {
                return NotFound();
            }

            // Load Tour navigation property
            var tourBookings = await _tourBookingService.GetTourBookingsWithToursAsync();
            tourBooking = tourBookings.FirstOrDefault(tb => tb.Id == id.Value);
            if (tourBooking == null)
            {
                return NotFound();
            }

            return View(tourBooking);
        }

        // GET: Admin/TourBookings/Create
        public async Task<IActionResult> Create()
        {
            var tours = await _tourService.GetAllToursAsync();
            ViewData["TourId"] = new SelectList(tours, "Id", "TourName");
            return View();
        }

        // POST: Admin/TourBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TourId,TourDate,CustomerPhone,CustomerEmail,NumberOfPeople,TotalPrice,ContactInfo,CustomerName,Status,Note")] TourBooking tourBooking)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _tourBookingService.CreateTourBookingAsync(tourBooking);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi tạo tour booking: {ex.Message}");
                }
            }

            var tours = await _tourService.GetAllToursAsync();
            ViewData["TourId"] = new SelectList(tours, "Id", "TourName", tourBooking.TourId);
            return View(tourBooking);
        }

        // GET: Admin/TourBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourBooking = await _tourBookingService.GetTourBookingByIdAsync(id.Value);
            if (tourBooking == null)
            {
                return NotFound();
            }

            var tours = await _tourService.GetAllToursAsync();
            ViewData["TourId"] = new SelectList(tours, "Id", "TourName", tourBooking.TourId);
            return View(tourBooking);
        }

        // POST: Admin/TourBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TourId,TourDate,CustomerPhone,CustomerEmail,NumberOfPeople,TotalPrice,ContactInfo,CustomerName,Status,Note")] TourBooking tourBooking)
        {
            if (id != tourBooking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _tourBookingService.UpdateTourBookingAsync(tourBooking);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi cập nhật tour booking: {ex.Message}");
                }
            }

            var tours = await _tourService.GetAllToursAsync();
            ViewData["TourId"] = new SelectList(tours, "Id", "TourName", tourBooking.TourId);
            return View(tourBooking);
        }

        // GET: Admin/TourBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourBookings = await _tourBookingService.GetTourBookingsWithToursAsync();
            var tourBooking = tourBookings.FirstOrDefault(tb => tb.Id == id.Value);
            if (tourBooking == null)
            {
                return NotFound();
            }

            return View(tourBooking);
        }

        // POST: Admin/TourBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _tourBookingService.DeleteTourBookingAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
