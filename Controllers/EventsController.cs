using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;

namespace VillaManagementWeb.Controllers
{
    public class EventsController : Controller
    {
        private readonly VillaDbContext _context;

        public EventsController(VillaDbContext context)
        {
            _context = context;
        }

        // 1. GET: Danh sách sự kiện + Form đặt lịch
        public async Task<IActionResult> Index()
        {
            var events = await _context.Events
                .OrderByDescending(e => e.EventDate)
                .ToListAsync();

            return View(events);
        }

        // 2. GET: Chi tiết sự kiện (nếu cần)
        public async Task<IActionResult> Details(int id)
        {
            var ev = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (ev == null) return NotFound();
            return View(ev);
        }

        // 3. POST: Xử lý Form đặt lịch
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Booking(string fullName, string email, string phone, string service, string message)
        {
            try
            {
                // -- XỬ LÝ LOGIC TÌM EVENT ID (Vì bảng Ticket bắt buộc phải có EventId) --
                // Mặc định lấy sự kiện mới nhất để gán vào vé
                var latestEvent = await _context.Events.OrderByDescending(e => e.EventDate).FirstOrDefaultAsync();
                int targetEventId = latestEvent != null ? latestEvent.Id : 1; // Nếu không có sự kiện nào thì gán tạm ID = 1 (Lưu ý: DB phải có Event Id 1)

                // -- TẠO VÉ MỚI --
                var newTicket = new Ticket
                {
                    EventId = targetEventId,
                    CustomerName = fullName,
                    CustomerEmail = email,
                    // Lưu ý: Phone và Message chưa có trong Model Ticket nên chưa lưu được vào DB
                    // Bạn cần thêm cột vào SQL nếu muốn lưu 2 trường này.

                    TicketType = service ?? "Standard", // Lấy từ dropdown form
                    Quantity = 1,
                    Price = 0, // Xử lý giá sau
                    BookingDate = DateTime.Now,
                    IsUsed = false,
                    QRCode = $"QR-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}" // Sinh mã ngẫu nhiên
                };

                _context.Tickets.Add(newTicket);
                await _context.SaveChangesAsync();

                // Lưu thông báo tạm để hiển thị bên trang Success (nếu muốn)
                TempData["BookingCode"] = newTicket.QRCode;

                // Chuyển hướng sang trang Success
                return RedirectToAction(nameof(Success));
            }
            catch (Exception ex)
            {
                // Nếu lỗi thì load lại trang Index
                return RedirectToAction(nameof(Index));
            }
        }

        // 4. TRANG THÔNG BÁO THÀNH CÔNG
        public IActionResult Success()
        {
            return View();
        }
    }
}