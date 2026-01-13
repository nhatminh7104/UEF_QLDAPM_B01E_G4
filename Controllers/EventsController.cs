using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace VillaManagementWeb.Controllers
{
    public class EventsController : Controller
    {
        private readonly VillaDbContext _context;

        public EventsController(VillaDbContext context)
        {
            _context = context;
        }

        // 1. GET: Danh sách sự kiện
        public async Task<IActionResult> Index()
        {
            var events = await _context.Events
                .OrderByDescending(e => e.EventDate)
                .ToListAsync();

            return View(events);
        }

        // 2. GET: Chi tiết sự kiện
        public async Task<IActionResult> Details(int id)
        {
            var ev = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);

            if (ev == null)
            {
                return NotFound();
            }

            return View(ev);
        }

        // 3. POST: Xử lý Form đặt lịch
        // Quan trọng: Thêm tham số eventId để biết đang đặt cho sự kiện nào
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Booking(int eventId, string fullName, string email, string phone, string service, string message)
        {
            try
            {
                // -- BƯỚC 1: KIỂM TRA SỰ KIỆN CÓ TỒN TẠI KHÔNG --
                // Tìm sự kiện theo ID được gửi từ Form
                var targetEvent = await _context.Events.FindAsync(eventId);

                if (targetEvent == null)
                {
                    // Trường hợp ID bị sai hoặc hack, quay về trang danh sách
                    return RedirectToAction(nameof(Index));
                }

                // -- BƯỚC 2: TẠO VÉ MỚI (Mapping dữ liệu vào bảng Tickets) --
                var newTicket = new Ticket
                {
                    EventId = targetEvent.Id, // Gán đúng ID sự kiện khách đang xem

                    CustomerName = fullName,
                    CustomerEmail = email,

                    // Nên tạm thời dữ liệu này chỉ gửi lên nhưng không lưu được vào DB.
                    // Nếu muốn lưu, bạn cần vào SQL Server thêm cột 'CustomerPhone' và 'Notes' cho bảng Tickets.

                    TicketType = service ?? targetEvent.Title, // Nếu không chọn dịch vụ thì lấy tên sự kiện
                    Quantity = 1,
                    Price = 0, // Giá vé (bạn có thể logic lấy giá từ bảng khác nếu cần)

                    BookingDate = DateTime.Now,
                    IsUsed = false,

                    // Tạo mã QR ngẫu nhiên
                    QRCode = $"QR-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}"
                };

                // -- BƯỚC 3: LƯU VÀO DATABASE --
                _context.Tickets.Add(newTicket);
                await _context.SaveChangesAsync();

                // Lưu mã vé để hiển thị trang Success
                TempData["BookingCode"] = newTicket.QRCode;
                TempData["BookingSuccess"] = true;

                // Chuyển hướng sang trang Success
                return RedirectToAction(nameof(Success));
            }
            catch (Exception ex)
            {
                // Nếu lỗi, có thể log lỗi ra console
                // Console.WriteLine(ex.Message);

                // Quay lại trang chi tiết sự kiện đó
                return RedirectToAction(nameof(Details), new { id = eventId });
            }
        }

        // 4. TRANG THÔNG BÁO THÀNH CÔNG
        public IActionResult Success()
        {
            return View();
        }
    }
}