using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.Repositories.Interfaces;

namespace VillaManagementWeb.Repositories.Implementations
{
    public class TourBookingRepository : GenericRepository<TourBooking>, ITourBookingRepository
    {
        public TourBookingRepository(VillaDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TourBooking>> GetTourBookingsWithToursAsync()
        {
            return await _dbSet
                .Include(tb => tb.Tour)
                .OrderByDescending(tb => tb.TourDate)
                .ToListAsync();
        }
    }
}

