using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data; // Thay bằng namespace Data thực tế của bạn
using VillaManagementWeb.Models; // Thay bằng namespace Models thực tế của bạn

namespace VillaManagementWeb.Controllers
{
    public class EventsController : Controller
    {
        private readonly VillaDbContext _context;

        public EventsController(VillaDbContext context)
        {
            _context = context;
        }

        // 1. GET: Events (Trang danh sách + Form đặt lịch)
        public async Task<IActionResult> Index()
        {
            var events = await _context.Events
                .OrderByDescending(e => e.EventDate)
                .ToListAsync();

            return View(events);
        }

        // 2. GET: Events/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var ev = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (ev == null) return NotFound();

            return View(ev);
        }

        // 3. POST: Xử lý Form đặt lịch
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Booking(string fullName, string email, string phone, string message)
        {
            // Ở đây bạn có thể lưu vào Database hoặc Gửi Email
            // Ví dụ: _context.Bookings.Add(new Booking { ... }); await _context.SaveChangesAsync();

            // Sau khi xử lý xong, chuyển hướng sang trang Success
            return RedirectToAction(nameof(Success));
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}