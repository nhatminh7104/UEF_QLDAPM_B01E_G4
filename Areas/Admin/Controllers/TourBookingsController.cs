using System;
using System.Collections.Generic;
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
    public class TourBookingsController : Controller
    {
        private readonly VillaDbContext _context;

        public TourBookingsController(VillaDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TourBookings
        public async Task<IActionResult> Index()
        {
            var villaDbContext = _context.TourBookings.Include(t => t.Tour);
            return View(await villaDbContext.ToListAsync());
        }

        // GET: Admin/TourBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourBooking = await _context.TourBookings
                .Include(t => t.Tour)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tourBooking == null)
            {
                return NotFound();
            }

            return View(tourBooking);
        }

        // GET: Admin/TourBookings/Create
        public IActionResult Create()
        {
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "Id");
            return View();
        }

        // POST: Admin/TourBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TourId,TourDate,NumberOfPeople,TotalPrice,ContactInfo,CustomerName,Status")] TourBooking tourBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tourBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "Id", tourBooking.TourId);
            return View(tourBooking);
        }

        // GET: Admin/TourBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourBooking = await _context.TourBookings.FindAsync(id);
            if (tourBooking == null)
            {
                return NotFound();
            }
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "Id", tourBooking.TourId);
            return View(tourBooking);
        }

        // POST: Admin/TourBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TourId,TourDate,NumberOfPeople,TotalPrice,ContactInfo,CustomerName,Status")] TourBooking tourBooking)
        {
            if (id != tourBooking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tourBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourBookingExists(tourBooking.Id))
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
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "Id", tourBooking.TourId);
            return View(tourBooking);
        }

        // GET: Admin/TourBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourBooking = await _context.TourBookings
                .Include(t => t.Tour)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var tourBooking = await _context.TourBookings.FindAsync(id);
            if (tourBooking != null)
            {
                _context.TourBookings.Remove(tourBooking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourBookingExists(int id)
        {
            return _context.TourBookings.Any(e => e.Id == id);
        }
    }
}
