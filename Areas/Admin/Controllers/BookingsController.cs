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
    public class BookingsController : Controller
    {
        private readonly VillaDbContext _context;

        public BookingsController(VillaDbContext context)
        {
            _context = context;
        }

        // GET: Controllers/Bookings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bookings.Include(b => b.Room).ToListAsync());
        }

        // GET: Controllers/Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Controllers/Bookings/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "RoomNumber");
            return View();
        }

        // POST: Controllers/Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoomId,CustomerName,CustomerPhone,AdultsCount,ChildrenCount,CustomerEmail,CheckIn,CheckOut,TotalAmount,Status,PaymentMethod,Notes,CreatedAt")] Booking booking)
        {
            // Server-side validation: CheckOut must be later than CheckIn
            if (booking.CheckOut <= booking.CheckIn)
            {
                ModelState.AddModelError("CheckOut", "Ngày check-out phải sau ngày check-in.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "RoomNumber", booking.RoomId);
            return View(booking);
        }

        // GET: Controllers/Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "RoomNumber", booking.RoomId);
            return View(booking);
        }

        // POST: Controllers/Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomId,CustomerName,CustomerPhone,AdultsCount,ChildrenCount,CustomerEmail,CheckIn,CheckOut,TotalAmount,Status,PaymentMethod,Notes,CreatedAt")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            // Server-side validation: CheckOut must be later than CheckIn
            if (booking.CheckOut <= booking.CheckIn)
            {
                ModelState.AddModelError("CheckOut", "Ngày check-out phải sau ngày check-in.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Get existing booking to avoid navigation property validation issues
                    var existingBooking = await _context.Bookings.FindAsync(id);
                    if (existingBooking == null)
                    {
                        return NotFound();
                    }

                    // Update only the properties that are bound
                    existingBooking.RoomId = booking.RoomId;
                    existingBooking.CustomerName = booking.CustomerName;
                    existingBooking.CustomerPhone = booking.CustomerPhone;
                    existingBooking.AdultsCount = booking.AdultsCount;
                    existingBooking.ChildrenCount = booking.ChildrenCount;
                    existingBooking.CustomerEmail = booking.CustomerEmail;
                    existingBooking.CheckIn = booking.CheckIn;
                    existingBooking.CheckOut = booking.CheckOut;
                    existingBooking.TotalAmount = booking.TotalAmount;
                    existingBooking.Status = booking.Status;
                    existingBooking.PaymentMethod = booking.PaymentMethod;
                    existingBooking.Notes = booking.Notes;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
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
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "RoomNumber", booking.RoomId);
            return View(booking);
        }

        // GET: Controllers/Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
