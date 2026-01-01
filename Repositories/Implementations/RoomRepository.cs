using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.Repositories.Interfaces;

namespace VillaManagementWeb.Repositories.Implementations
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(VillaDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Room>> GetActiveRoomsAsync()
        {
            return await _dbSet
                .Where(r => r.IsActive == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetRoomsWithBookingsAsync()
        {
            return await _dbSet
                .Include(r => r.Bookings)
                .ToListAsync();
        }
    }
}

