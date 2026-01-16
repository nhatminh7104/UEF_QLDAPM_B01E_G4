using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Repositories.Interfaces;

namespace VillaManagementWeb.Admin.Repositories.Implementations
{
    public class RoomsRepository : IRoomsRepository
    {
        private readonly VillaDbContext _context;
        public RoomsRepository(VillaDbContext context) => _context = context;

        public async Task<IEnumerable<Room>> GetAllRoomsAsync() =>
            await _context.Rooms.Include(r => r.RoomImages).ToListAsync();

        public async Task<Room?> GetRoomByIdAsync(int id) =>
            await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);

        // --- TRIỂN KHAI CÁC HÀM CÒN THIẾU ---

        public async Task<Room?> GetRoomWithImagesAsync(int id)
        {
            return await _context.Rooms
                .Include(r => r.RoomImages)
                .Include(r => r.RoomCategory) // Thường cần load thêm Category để hiển thị
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<RoomCategory>> GetRoomCategoriesAsync()
        {
            return await _context.RoomCategories.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<RoomImage?> GetRoomImageByIdAsync(int imageId)
        {
            return await _context.RoomImages.FindAsync(imageId);
        }

        public void DeleteImage(RoomImage image)
        {
            _context.RoomImages.Remove(image);
        }

        // --- CÁC HÀM CƠ BẢN ---

        public async Task AddAsync(Room room) => await _context.Rooms.AddAsync(room);

        public void Update(Room room)
        {
            _context.Rooms.Update(room);
        }

        public void Delete(Room room) => _context.Rooms.Remove(room);

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;

        public async Task<bool> ExistsAsync(int id) => await _context.Rooms.AnyAsync(r => r.Id == id);
    }
}