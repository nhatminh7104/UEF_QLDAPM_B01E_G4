using VillaManagementWeb.Models;

namespace VillaManagementWeb.ViewModels
{
    public class DashboardViewModel
    {
        // 4 Thẻ thống kê chính
        public decimal TotalRevenue { get; set; }      // Tổng doanh thu
        public int NewBookingsToday { get; set; }      // Đơn mới hôm nay
        public int TotalCheckInsToday { get; set; }    // Khách check-in hôm nay
        public double OccupancyRate { get; set; }      // Tỷ lệ lấp đầy phòng (%)

        // Danh sách đơn gần đây
        public IEnumerable<Booking> RecentBookings { get; set; } = new List<Booking>();

        // Dữ liệu cho biểu đồ (Chart.js)
        public List<string> ChartLabels { get; set; } = new List<string>(); // ["20/10", "21/10",...]
        public List<decimal> ChartData { get; set; } = new List<decimal>(); // [5000000, 0, 1500000...]
    }
}