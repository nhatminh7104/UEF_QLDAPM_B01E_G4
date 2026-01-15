using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.ViewModels;

namespace VillaManagementWeb.Controllers
{
    public class TourBookingsController : Controller
    {
        private readonly VillaDbContext _context;
        public TourBookingsController(VillaDbContext context)
        {
            _context = context;
        }

        // GET: TourBookings/Create?tourId=1
        [HttpGet]
        public async Task<IActionResult> Create(int tourId, int guests = 1)
        {
            var tour = await _context.Tours.FindAsync(tourId);
            if (tour == null) return NotFound();

            // Đổ dữ liệu Tour vào ViewModel để View hiển thị đẹp
            var model = new TourCheckoutVM
            {
                TourId = tour.Id,
                TourName = tour.TourName,
                ImageUrl = tour.ImageUrl ?? "/images/no-image.png",
                PricePerPerson = tour.PricePerPerson,
                Duration = tour.DurationHours,
                NumberOfPeople = guests,
                TotalAmount = tour.PricePerPerson * guests, // Tạm tính
                TourDate = DateTime.Now.AddDays(1)
            };

            return View(model);
        }

        // POST: TourBookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TourCheckoutVM model)
        {
            if (ModelState.IsValid)
            {
                var tour = await _context.Tours.FindAsync(model.TourId);
                if (tour == null) return NotFound();

                // 1. Map dữ liệu từ VM sang Model Entity
                var booking = new TourBooking
                {
                    TourId = model.TourId,
                    CustomerName = model.CustomerName,
                    CustomerPhone = model.CustomerPhone,
                    CustomerEmail = model.CustomerEmail,
                    TourDate = model.TourDate,
                    NumberOfPeople = model.NumberOfPeople,
                    Note = model.Note,
                    CreatedAt = DateTime.Now,
                    Status = "Pending",
                    // 2. Tính lại giá ở Server để bảo mật (Logic cũ của bạn)
                    TotalPrice = tour.PricePerPerson * model.NumberOfPeople
                };

                _context.Add(booking);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Đặt lịch tour thành công!";
                // Chuyển sang trang Success chung hoặc trang chủ
                return RedirectToAction("BookingSuccess", "Booking", new { id = booking.Id });
            }

            // Nếu lỗi, load lại thông tin tour để hiển thị lại form
            var reloadTour = await _context.Tours.FindAsync(model.TourId);
            if (reloadTour != null)
            {
                model.TourName = reloadTour.TourName;
                model.ImageUrl = reloadTour.ImageUrl;
                model.PricePerPerson = reloadTour.PricePerPerson;
            }
            return View(model);
        }
    }
}