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

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TicketsController : Controller
    {
        private readonly VillaDbContext _context;

        public TicketsController(VillaDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Tickets
        public async Task<IActionResult> Index()
        {
            // Include Event để hiển thị tên sự kiện trong bảng
            var tickets = _context.Tickets.Include(t => t.Event);
            return View(await tickets.ToListAsync());
        }

        // GET: Admin/Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var ticket = await _context.Tickets
                .Include(t => t.Event)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null) return NotFound();

            return View(ticket);
        }

        // GET: Admin/Tickets/Create
        public IActionResult Create()
        {
            // SỬA: Hiển thị "Title" thay vì "Id" để người dùng dễ chọn
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title");
            return View();
        }

        // POST: Admin/Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventId,TicketType,Price,Quantity,BookingDate,CustomerName,CustomerPhone,CustomerEmail,Status")] Ticket ticket)
        {
            // 1. Tự động sinh mã QR (Thay thế giá trị placeholder từ View)
            // Format ví dụ: TKT-20231025-A1B2
            string uniqueCode = Guid.NewGuid().ToString().Substring(0, 4).ToUpper();
            ticket.QRCode = $"TKT-{DateTime.Now:yyyyMMdd}-{uniqueCode}";

            // 2. Kiểm tra nếu BookingDate chưa chọn thì gán ngày hiện tại
            if (ticket.BookingDate == default)
            {
                ticket.BookingDate = DateTime.Now;
            }

            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Nếu lỗi, load lại dropdown với Title
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title", ticket.EventId);
            return View(ticket);
        }

        // GET: Admin/Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            // SỬA: Hiển thị Title
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title", ticket.EventId);
            return View(ticket);
        }

        // POST: Admin/Tickets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventId,TicketType,Price,Quantity,BookingDate,CustomerName,CustomerPhone,CustomerEmail,Status")] Ticket ticket)
        {
            if (id != ticket.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title", ticket.EventId);
            return View(ticket);
        }

        // GET: Admin/Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ticket = await _context.Tickets
                .Include(t => t.Event)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null) return NotFound();

            return View(ticket);
        }

        // POST: Admin/Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}