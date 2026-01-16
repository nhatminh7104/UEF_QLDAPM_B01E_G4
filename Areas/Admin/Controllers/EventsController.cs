using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Services.Interfaces;
using VillaManagementWeb.Services.Interfaces; // Đảm bảo namespace này khớp với project Admin của bạn

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EventsController : Controller
    {
        private readonly IEventsService _eventsService;
        private readonly ITicketService _ticketService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EventsController(IEventsService eventsService, ITicketService ticketService, IWebHostEnvironment webHostEnvironment)
        {
            _eventsService = eventsService;
            _ticketService = ticketService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Events
        public async Task<IActionResult> Index() => View(await _eventsService.GetAllEventsAsync());

        // GET: Admin/Events/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var @event = await _eventsService.GetEventByIdAsync(id);
            if (@event == null) return NotFound();
            return View(@event);
        }

        // GET: Admin/Events/Create
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event @event, IFormFile? ImageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        @event.ImageUrl = await SaveImage(ImageFile);
                    }

                    await _eventsService.CreateEventAsync(@event);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi tạo sự kiện: {ex.Message}");
                }
            }
            return View(@event);
        }

        // GET: Admin/Events/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var @event = await _eventsService.GetEventByIdAsync(id);
            if (@event == null) return NotFound();
            return View(@event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event @event, IFormFile? ImageFile)
        {
            if (id != @event.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        @event.ImageUrl = await SaveImage(ImageFile);
                    }

                    var success = await _eventsService.UpdateEventAsync(@event);
                    if (success) return RedirectToAction(nameof(Index));

                    ModelState.AddModelError("", "Cập nhật không thành công.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi cập nhật: {ex.Message}");
                }
            }
            return View(@event);
        }

        // GET: Admin/Events/Delete/5
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

        // GET: Admin/Events/Tickets/5
        public async Task<IActionResult> Tickets(int id)
        {
            var @event = await _eventsService.GetEventByIdAsync(id);
            if (@event == null) return NotFound();

            var tickets = await _ticketService.GetTicketsByEventIdAsync(id);
            ViewBag.Event = @event;
            return View(tickets);
        }

        // Hàm phụ hỗ trợ lưu ảnh để code gọn hơn
        private async Task<string> SaveImage(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "events");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            string filePath = Path.Combine(uploadFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return "/images/events/" + fileName;
        }
    }
}