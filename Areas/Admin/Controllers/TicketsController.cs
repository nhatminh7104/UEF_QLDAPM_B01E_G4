using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Services.Interfaces;

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TicketsController : Controller
    {
        private readonly ITicketsService _ticketsService;
        private readonly IEventsService _eventsService;

        public TicketsController(
            ITicketsService ticketsService,
            IEventsService eventsService)
        {
            _ticketsService = ticketsService;
            _eventsService = eventsService;
        }

        // GET: Admin/Tickets
        public async Task<IActionResult> Index()
        {
            return View(await _ticketsService.GetAllTicketsAsync());
        }

        // GET: Admin/Tickets/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _ticketsService.GetTicketByIdAsync(id);
            if (ticket == null) return NotFound();

            return View(ticket);
        }

        // GET: Admin/Tickets/Create
        public async Task<IActionResult> Create()
        {
            ViewData["EventId"] = new SelectList(
                await _eventsService.GetAllEventsAsync(),
                "Id",
                "Title");

            return View();
        }

        // POST: Admin/Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            // Sinh QR Code
            string uniqueCode = Guid.NewGuid().ToString("N")[..6].ToUpper();
            ticket.QRCode = $"TKT-{DateTime.Now:yyyyMMdd}-{uniqueCode}";

            // Gán ngày hiện tại nếu chưa có
            if (ticket.BookingDate == default)
            {
                ticket.BookingDate = DateTime.Now;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _ticketsService.CreateTicketAsync(ticket);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewData["EventId"] = new SelectList(
                await _eventsService.GetAllEventsAsync(),
                "Id",
                "Title",
                ticket.EventId);

            return View(ticket);
        }

        // GET: Admin/Tickets/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _ticketsService.GetTicketByIdAsync(id);
            if (ticket == null) return NotFound();

            ViewData["EventId"] = new SelectList(
                await _eventsService.GetAllEventsAsync(),
                "Id",
                "Title",
                ticket.EventId);

            return View(ticket);
        }

        // POST: Admin/Tickets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id != ticket.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _ticketsService.UpdateTicketAsync(ticket);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewData["EventId"] = new SelectList(
                await _eventsService.GetAllEventsAsync(),
                "Id",
                "Title",
                ticket.EventId);

            return View(ticket);
        }

        // GET: Admin/Tickets/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _ticketsService.GetTicketByIdAsync(id);
            if (ticket == null) return NotFound();

            return View(ticket);
        }

        // POST: Admin/Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ticketsService.DeleteTicketAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
