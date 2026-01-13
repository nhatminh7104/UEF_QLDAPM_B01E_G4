using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Services.Interfaces;

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EventsController : Controller
    {
        private readonly IEventsService _eventsService;

        public EventsController(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        public async Task<IActionResult> Index() => View(await _eventsService.GetAllEventsAsync());

        public async Task<IActionResult> Details(int id)
        {
            var @event = await _eventsService.GetEventByIdAsync(id);
            if (@event == null) return NotFound();
            return View(@event);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event @event)
        {
            if (ModelState.IsValid)
            {
                await _eventsService.CreateEventAsync(@event);
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var @event = await _eventsService.GetEventByIdAsync(id);
            if (@event == null) return NotFound();
            return View(@event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event @event)
        {
            if (id != @event.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var success = await _eventsService.UpdateEventAsync(@event);
                if (success) return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", "Cập nhật không thành công.");
            }
            return View(@event);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var @event = await _eventsService.GetEventByIdAsync(id);
            if (@event == null) return NotFound();
            return View(@event);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eventsService.DeleteEventAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}