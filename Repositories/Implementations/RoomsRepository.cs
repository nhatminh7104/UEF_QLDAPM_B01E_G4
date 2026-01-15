using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.Repositories.Interfaces;
using VillaManagementWeb.Repositories.Implementations;

namespace VillaManagementWeb.Repositories.Implementations
{
    public class RoomsRepository : GenericRepository<Room>, IRoomsRepository
    {
        public RoomsRepository(VillaDbContext context) : base(context)
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
                .Include(r => r.RoomBookings)
                .ToListAsync();
        }
    }
}

