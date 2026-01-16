using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.Repositories.Interfaces;

namespace VillaManagementWeb.Repositories.Implementations
{
    public class RoomBookingsRepository : GenericRepository<RoomBooking>, IRoomBookingsRepository
    {
        public RoomBookingsRepository(VillaDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RoomBooking>> GetRoomBookingsWithRoomsAsync()
        {
            return await _dbSet
                .Include(b => b.Room)
                .ToListAsync();
        }
    }
}

