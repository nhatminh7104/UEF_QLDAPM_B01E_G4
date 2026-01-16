using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.ViewModels;

namespace VillaManagementWeb.Controllers
{
    public class BookingController : Controller
    {
        private readonly VillaDbContext _context;

        public BookingController(VillaDbContext context)
        {
            _context = context;
        }

        // GET: /Booking
        [HttpGet]
        public async Task<IActionResult> Index(int? roomId, DateTime? checkIn, DateTime? checkOut, int? adults, int? children)
        {
            if (roomId == null) return RedirectToAction("Index", "Home");

            // 1. ÉP KIỂU GIỜ: Luôn là 14:00 Nhận và 12:00 Trả
            // Nếu checkIn có giá trị (từ URL) thì lấy Date + 14h, nếu không thì lấy mặc định
            var start = checkIn.HasValue ? checkIn.Value.Date.AddHours(14) : DateTime.Now.Date.AddDays(1).AddHours(14);
            var end = checkOut.HasValue ? checkOut.Value.Date.AddHours(12) : DateTime.Now.Date.AddDays(2).AddHours(12);

            // 2. Load thông tin
            var model = await LoadRoomDetails(roomId.Value, start, end);
            if (model == null) return NotFound();

            // 3. Fill dữ liệu
            model.BookingRequest.CheckIn = start;
            model.BookingRequest.CheckOut = end;
            model.BookingRequest.AdultsCount = adults ?? model.CapacityAdults;
            model.BookingRequest.ChildrenCount = children ?? 0;
            model.BookingRequest.TotalAmount = model.PricePerNight;

            return View(model);
        }

        // ---API ĐỂ JS GỌI KIỂM TRA PHÒNG TRỐNG ---
        [HttpGet]
        public async Task<IActionResult> CheckAvailability(int roomId, DateTime checkIn, DateTime checkOut)
        {
            // Ép kiểu giờ chuẩn khách sạn
            var start = checkIn.Date.AddHours(14);
            var end = checkOut.Date.AddHours(12);
            if (end <= start)
            {
                end = start.AddDays(1).Date.AddHours(12);
                // Tùy chọn: Thêm thông báo nếu muốn hiển thị ra View
                // ModelState.AddModelError("", "Ngày trả phòng đã được tự động điều chỉnh.");
            }
            // Tái sử dụng hàm logic cũ
            var model = await LoadRoomDetails(roomId, start, end);

            if (model == null) return Json(new { maxSlots = 0 });

            return Json(new { maxSlots = model.MaxAvailableSlots });
        }

        // POST: Xử lý đặt phòng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomBookingDetailVM model)
        {
            // 1. Dọn dẹp ModelState (Bỏ qua các trường hiển thị không cần validate)
            ModelState.Remove("RoomName");
            ModelState.Remove("CategoryName");
            ModelState.Remove("ImageUrl");
            ModelState.Remove("Description");
            ModelState.Remove("Amenities");
            ModelState.Remove("GalleryImages");
            ModelState.Remove("BookingRequest.Status");
            ModelState.Remove("BookingRequest.PaymentMethod");
            ModelState.Remove("BookingRequest.Room");
            ModelState.Remove("BookingRequest.Customer"); // Nếu có relation

            var booking = model.BookingRequest;

            // 2. Lấy thông tin phòng gốc để VALIDATE SỨC CHỨA
            var room = await _context.Rooms.FindAsync(booking.RoomId);
            if (room == null) return NotFound();

            // --- START: VALIDATION LOGIC ---

            // Check ngày
            if (booking.CheckIn >= booking.CheckOut)
            {
                ModelState.AddModelError("BookingRequest.CheckOut", "Ngày trả phòng phải sau ngày nhận phòng.");
            }

            // Check quá số người (Logic bạn cần thêm)
            if (booking.AdultsCount > room.Capacity)
            {
                ModelState.AddModelError("BookingRequest.AdultsCount", $"Phòng này chỉ chứa tối đa {room.Capacity} người lớn.");
            }

            if (booking.ChildrenCount > room.CapacityChildren)
            {
                ModelState.AddModelError("BookingRequest.ChildrenCount", $"Phòng này chỉ chứa tối đa {room.CapacityChildren} trẻ em.");
            }
            // --- END: VALIDATION LOGIC ---

            if (ModelState.IsValid)
            {
                // 3. LOGIC TỰ ĐỘNG TÌM PHÒNG TRỐNG (AUTO-ASSIGN)

                // Tìm tất cả ID phòng cùng loại, cùng nhóm
                var groupRoomIds = await _context.Rooms
                    .Where(r => r.RoomCategoryId == room.RoomCategoryId
                             && r.Type == room.Type
                             && r.IsActive)
                    .Select(r => r.Id)
                    .ToListAsync();

                // Tìm các phòng đang bận
                var busyRoomIds = await _context.Bookings
                    .Where(b => groupRoomIds.Contains(b.RoomId)
                             && b.Status != "Cancelled"
                             && b.CheckIn < booking.CheckOut
                             && b.CheckOut > booking.CheckIn) // Logic giao nhau thời gian
                    .Select(b => b.RoomId)
                    .Distinct()
                    .ToListAsync();

                // Danh sách phòng trống
                var availableRoomIds = groupRoomIds.Except(busyRoomIds).ToList();

                if (availableRoomIds.Count == 0)
                {
                    ModelState.AddModelError("", "Rất tiếc, tất cả phòng loại này đã hết trong ngày bạn chọn.");
                }
                else
                {
                    // Nếu phòng khách chọn ban đầu bị bận, đổi sang phòng trống khác
                    if (!availableRoomIds.Contains(booking.RoomId))
                    {
                        booking.RoomId = availableRoomIds.First();
                    }

                    // 4. Tính toán và Lưu
                    booking.CreatedAt = DateTime.Now;
                    booking.Status = "Pending";
                    if (string.IsNullOrEmpty(booking.PaymentMethod)) booking.PaymentMethod = "BankTransfer";

                    // Tính tiền lại (Security)
                    var nights = Math.Ceiling((booking.CheckOut - booking.CheckIn).TotalDays);
                    if (nights < 1) nights = 1;
                    booking.TotalAmount = room.PricePerNight * (decimal)nights;

                    try
                    {
                        _context.Bookings.Add(booking);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("BookingSuccess", new { id = booking.Id });
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Lỗi hệ thống khi lưu đơn: " + ex.Message);
                    }
                }
            }

            // 5. NẾU CÓ LỖI: Load lại dữ liệu để hiển thị lại View
            // Quan trọng: Phải load lại thông tin phòng để View không bị lỗi Null
            var reloadedModel = await LoadRoomDetails(booking.RoomId, booking.CheckIn, booking.CheckOut);
            if (reloadedModel != null)
            {
                // Gán lại dữ liệu khách vừa nhập để không bị mất
                reloadedModel.BookingRequest = booking;
                return View("Index", reloadedModel);
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Hiển thị trang Xác nhận / Cảm ơn
        [HttpGet]
        public async Task<IActionResult> BookingSuccess(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Room)
                .ThenInclude(r => r.RoomCategory) // Lấy thêm Category để hiển thị cho đẹp
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null) return NotFound();

            return View(booking);
        }
       

        // 2. Hàm kiểm tra phòng trống trong Database
        private async Task<RoomBookingDetailVM?> LoadRoomDetails(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var room = await _context.Rooms
                .Include(r => r.RoomImages)
                .Include(r => r.RoomCategory)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null) return null;

            // A. Tổng số phòng vật lý của loại này
            var allRoomsInGroup = await _context.Rooms
                .Where(r => r.RoomCategoryId == room.RoomCategoryId
                         && r.Type == room.Type
                         && r.IsActive)
                .Select(r => r.Id)
                .ToListAsync();

            int totalInventory = allRoomsInGroup.Count;

            // B. Số phòng đang bận trong khoảng thời gian này
            // Logic: Có booking nào (ko phải Cancelled) chen vào giữa checkIn & checkOut không?
            int busyCount = await _context.Bookings
                .Where(b => allRoomsInGroup.Contains(b.RoomId)
                         && b.Status != "Cancelled"
                         && b.CheckIn < checkOut
                         && b.CheckOut > checkIn)
                .Select(b => b.RoomId)
                .Distinct()
                .CountAsync();

            // C. Số phòng còn lại thực tế
            int availableCount = totalInventory - busyCount;

            return new RoomBookingDetailVM
            {
                RoomId = room.Id,
                RoomName = room.RoomNumber,
                CategoryName = room.RoomCategory?.Name ?? "General",
                ImageUrl = room.ImageUrl ?? "/images/no-image.png",
                PricePerNight = room.PricePerNight,
                Description = room.Description,
                Amenities = room.RoomCategory?.Amenities?.Split('\n').ToList() ?? new List<string>(),
                GalleryImages = room.RoomImages.Select(x => x.ImageUrl).ToList(),
                CapacityAdults = room.Capacity,
                CapacityChildren = room.CapacityChildren,

                // Hiển thị số lượng còn trống cho khách chọn (Nếu < 0 thì để 0)
                MaxAvailableSlots = availableCount > 0 ? availableCount : 0,

                BookingRequest = new Booking { RoomId = room.Id }
            };
        }
    }
}