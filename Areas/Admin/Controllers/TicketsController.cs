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

        public TicketsController(ITicketsService ticketsService, IEventsService eventsService)
        {
            _ticketsService = ticketsService;
            _eventsService = eventsService;
        }

        public async Task<IActionResult> Index() => View(await _ticketsService.GetAllTicketsAsync());

        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _ticketsService.GetTicketByIdAsync(id);
            return ticket == null ? NotFound() : View(ticket);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["EventId"] = new SelectList(await _eventsService.GetAllEventsAsync(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
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
            ViewData["EventId"] = new SelectList(await _eventsService.GetAllEventsAsync(), "Id", "Title", ticket.EventId);
            return View(ticket);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _ticketsService.GetTicketByIdAsync(id);
            if (ticket == null) return NotFound();

            ViewData["EventId"] = new SelectList(await _eventsService.GetAllEventsAsync(), "Id", "Title", ticket.EventId);
            return View(ticket);
        }

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
            ViewData["EventId"] = new SelectList(await _eventsService.GetAllEventsAsync(), "Id", "Title", ticket.EventId);
            return View(ticket);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ticketsService.DeleteTicketAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}