using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.ViewModels;

namespace VillaManagementWeb.Controllers
{
    public class TicketsController : Controller
    {
        private readonly VillaDbContext _context;
        public TicketsController(VillaDbContext context)
        {
            _context = context;
        }

        // GET: Tickets/Create?eventId=1
        [HttpGet]
        public async Task<IActionResult> Create(int eventId)
        {
            var evt = await _context.Events.FindAsync(eventId);
            if (evt == null) return NotFound();

            var model = new TicketCheckoutVM
            {
                EventId = evt.Id,
                EventTitle = evt.Title,
                ImageUrl = evt.ImageUrl ?? "/images/no-image.png",
                EventDate = evt.StartDate ?? DateTime.Now,
                Location = evt.Location,
                Quantity = 1,
                // Giá mặc định hiển thị
                UnitPrice = 100000
            };
            return View(model);
        }

        // POST: Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketCheckoutVM model)
        {
            if (ModelState.IsValid)
            {
                // 1. Map từ VM sang Entity Ticket
                var ticket = new Ticket
                {
                    EventId = model.EventId,
                    CustomerName = model.CustomerName,
                    CustomerEmail = model.CustomerEmail,
                    CustomerPhone = model.CustomerPhone,
                    TicketType = model.TicketType,
                    Quantity = model.Quantity,

                    // SỬA DÒNG NÀY: Đổi PurchaseDate -> BookingDate
                    BookingDate = DateTime.Now,

                    QRCode = Guid.NewGuid().ToString()
                };

                // 2. Logic tính giá (Logic cũ của bạn)
                decimal basePrice = 100000; // Hoặc lấy từ DB nếu có cột Price
                if (model.TicketType == "VIP") basePrice = 200000;

                ticket.Price = basePrice * model.Quantity;

                _context.Add(ticket);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Đặt vé sự kiện thành công!";
                return RedirectToAction("BookingSuccess", "Booking", new { id = ticket.Id });
            }
            return View(model);
        }
    }
}