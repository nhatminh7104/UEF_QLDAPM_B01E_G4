using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Repositories.Interfaces;

namespace VillaManagementWeb.Admin.Repositories.Implementations
{
    public class TourBookingsRepository : ITourBookingsRepository
    {
        private readonly VillaDbContext _context;
        public TourBookingsRepository(VillaDbContext context) => _context = context;

        public async Task<IEnumerable<TourBooking>> GetAllWithToursAsync() =>
            await _context.TourBookings.Include(t => t.Tour).ToListAsync();

        public async Task<TourBooking?> GetByIdWithTourAsync(int id) =>
            await _context.TourBookings.Include(t => t.Tour).FirstOrDefaultAsync(m => m.Id == id);

        public async Task AddAsync(TourBooking tourBooking) => await _context.TourBookings.AddAsync(tourBooking);

        public void Update(TourBooking tourBooking) => _context.TourBookings.Update(tourBooking);

        public void Delete(TourBooking tourBooking) => _context.TourBookings.Remove(tourBooking);

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;

        public async Task<bool> ExistsAsync(int id) => await _context.TourBookings.AnyAsync(e => e.Id == id);
    }
}