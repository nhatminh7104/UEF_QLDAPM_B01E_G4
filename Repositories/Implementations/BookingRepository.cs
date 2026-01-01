using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.Repositories.Interfaces;

namespace VillaManagementWeb.Repositories.Implementations
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(VillaDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Booking>> GetBookingsWithRoomsAsync()
        {
            return await _dbSet
                .Include(b => b.Room)
                .ToListAsync();
        }
    }
}

