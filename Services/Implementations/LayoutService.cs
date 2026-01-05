using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
namespace VillaManagementWeb.Services.Implementations
{
    public class LayoutService
    {
        private readonly VillaDbContext _context;

        public LayoutService(VillaDbContext context)
        {
            _context = context;
        }

        //Hàm lấy danh sách Category(Distinct để lọc trùng)
        public async Task<List<string>> GetRoomCategories()
        {
            return await _context.RoomCategories
                                 //.Where(r => !string.IsNullOrEmpty(r.Category)) // Bỏ category rỗng
                                 .Select(r => r.Name)
                                 .Distinct()
                                 .ToListAsync();
        }
    }
}
