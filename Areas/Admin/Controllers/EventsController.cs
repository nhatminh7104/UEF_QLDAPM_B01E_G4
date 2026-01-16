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
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;
        private readonly ITicketService _ticketService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EventsController(IEventService eventService, ITicketService ticketService, IWebHostEnvironment webHostEnvironment)
        {
            _eventService = eventService;
            _ticketService = ticketService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Events
        public async Task<IActionResult> Index()
        {
            return View(await _eventService.GetAllEventsAsync());
        }

        // GET: Admin/Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _eventService.GetEventByIdAsync(id.Value);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Admin/Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,EventDate,StartDate,EndDate,Location,TotalTickets,ImageUrl")] Event @event)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _eventService.CreateEventAsync(@event);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(@event);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi tạo sự kiện: {ex.Message}");
                    return View(@event);
                }
            }
            return View(@event);
        }

        // GET: Admin/Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _eventService.GetEventByIdAsync(id.Value);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Admin/Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,EventDate,StartDate,EndDate,Location,TotalTickets,ImageUrl")] Event @event, IFormFile? ImageFile)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // --- BẮT ĐẦU ĐOẠN XỬ LÝ ẢNH ---
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        // 1. Tạo tên file duy nhất để tránh trùng lặp
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);

                        // 2. Xác định đường dẫn lưu (ví dụ: wwwroot/images/events)
                        string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "events");

                        // Tạo thư mục nếu chưa có
                        if (!Directory.Exists(uploadFolder))
                            Directory.CreateDirectory(uploadFolder);

                        string filePath = Path.Combine(uploadFolder, fileName);

                        // 3. Lưu file vào server
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(stream);
                        }

                        // 4. Xóa ảnh cũ nếu cần (Tùy chọn - để tiết kiệm dung lượng)
                        // if (!string.IsNullOrEmpty(@event.ImageUrl)) { ... logic xóa file cũ ... }

                        // 5. Cập nhật đường dẫn ảnh mới vào Model
                        @event.ImageUrl = "/images/events/" + fileName;
                    }
                    // Nếu ImageFile == null, @event.ImageUrl sẽ giữ nguyên giá trị cũ (nhờ input hidden ở View)
                    // --- KẾT THÚC ĐOẠN XỬ LÝ ẢNH ---

                    await _eventService.UpdateEventAsync(@event);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi cập nhật sự kiện: {ex.Message}");
                }
            }

            // Nếu ModelState lỗi hoặc có Exception, trả về View để user sửa
            return View(@event);
        }

        // GET: Admin/Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _eventService.GetEventByIdAsync(id.Value);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Admin/Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _eventService.DeleteEventAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Events/Tickets/5
        public async Task<IActionResult> Tickets(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _eventService.GetEventByIdAsync(id.Value);
            if (@event == null)
            {
                return NotFound();
            }

            var tickets = await _ticketService.GetTicketsByEventIdAsync(id.Value);
            ViewBag.Event = @event;
            return View(tickets);
        }
    }
}
