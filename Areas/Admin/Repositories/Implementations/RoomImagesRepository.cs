using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Repositories.Interfaces;

namespace VillaManagementWeb.Admin.Repositories.Implementations
{
    public class RoomImagesRepository : IRoomImagesRepository
    {
        private readonly VillaDbContext _context;
        public RoomImagesRepository(VillaDbContext context) => _context = context;

        public async Task<IEnumerable<RoomImage>> GetAllWithRoomAsync() =>
            await _context.RoomImages.Include(r => r.Room).ToListAsync();

        public async Task<RoomImage?> GetByIdAsync(int id) =>
            await _context.RoomImages.Include(r => r.Room).FirstOrDefaultAsync(m => m.Id == id);

        public async Task AddAsync(RoomImage roomImage) => await _context.RoomImages.AddAsync(roomImage);

        public void Update(RoomImage roomImage) => _context.RoomImages.Update(roomImage);

        public void Delete(RoomImage roomImage) => _context.RoomImages.Remove(roomImage);

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;

        public async Task<bool> ExistsAsync(int id) => await _context.RoomImages.AnyAsync(e => e.Id == id);
    }
}