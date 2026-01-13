using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly VillaDbContext _context;

        public DashboardController(VillaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // 1. Thống kê tổng quát 
            var totalRevenue = await _context.RoomBookings
                .Where(b => b.Status == "Confirmed")
                .SumAsync(b => (decimal?)b.TotalAmount) ?? 0;

            ViewBag.TotalRevenue = totalRevenue;
            ViewBag.TotalRoomBookings = await _context.RoomBookings.CountAsync();
            ViewBag.TotalRooms = await _context.Rooms.CountAsync(r => r.IsActive);
            ViewBag.PendingRoomBookings = await _context.RoomBookings.CountAsync(b => b.Status == "Pending");

            // 2. Lấy 5 đơn đặt phòng mới nhất
            var recentRoomBookings = await _context.RoomBookings
                .Include(b => b.Room)
                .OrderByDescending(b => b.CreatedAt)
                .Take(5)
                .ToListAsync();

            return View(recentRoomBookings);
        }
    }
}