using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Services.Interfaces;
using VillaManagementWeb.ViewModels;

namespace VillaManagementWeb.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly VillaDbContext _context;

        public DashboardService(VillaDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync()
        {
            var model = new DashboardViewModel();
            var today = DateTime.Today;
            var sevenDaysAgo = today.AddDays(-6);

            // 1. Lấy dữ liệu Booking thô (chỉ lấy những đơn không bị hủy)
            var activeBookings = await _context.RoomBookings
                .Include(b => b.Room)
                .Where(b => b.Status != "Cancelled")
                .ToListAsync();

            // 2. Tính toán các chỉ số
            // Tổng doanh thu (Phòng)
            decimal roomRevenue = activeBookings.Sum(b => b.TotalAmount);

            // Nếu bạn đã có bảng TourBookings, hãy uncomment dòng dưới:
            // decimal tourRevenue = await _context.TourBookings.Where(t => t.Status != "Cancelled").SumAsync(t => t.TotalPrice);
            decimal tourRevenue = 0;

            model.TotalRevenue = roomRevenue + tourRevenue;
            model.NewBookingsToday = activeBookings.Count(b => b.CreatedAt.Date == today);
            model.TotalCheckInsToday = activeBookings.Count(b => b.CheckIn.Date == today);

            // 3. Tính tỷ lệ lấp đầy (Occupancy Rate)
            int totalRooms = await _context.Rooms.CountAsync(r => r.IsActive);
            int occupiedRooms = activeBookings.Count(b =>
                b.Status == "Confirmed" &&
                b.CheckIn <= DateTime.Now &&
                b.CheckOut >= DateTime.Now);

            model.OccupancyRate = totalRooms > 0 ? Math.Round((double)occupiedRooms / totalRooms * 100, 1) : 0;

            // 4. Lấy 5 đơn mới nhất
            model.RecentBookings = activeBookings
                .OrderByDescending(b => b.CreatedAt)
                .Take(5)
                .ToList();

            // 5. Chuẩn bị dữ liệu biểu đồ (7 ngày gần nhất)
            for (int i = 0; i < 7; i++)
            {
                var date = sevenDaysAgo.AddDays(i);
                model.ChartLabels.Add(date.ToString("dd/MM"));

                // Doanh thu ngày đó = Tổng tiền các đơn TẠO vào ngày đó (Hoặc tính theo ngày Check-in tùy nghiệp vụ)
                // Ở đây tôi tính theo ngày tạo đơn (Cash flow dự kiến)
                var dailyRevenue = activeBookings
                    .Where(b => b.CreatedAt.Date == date.Date)
                    .Sum(b => b.TotalAmount);

                model.ChartData.Add(dailyRevenue);
            }

            return model;
        }
    }
}