using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Repositories.Interfaces;

namespace VillaManagementWeb.Admin.Repositories.Implementations
{
    public class ToursRepository : IToursRepository
    {
        private readonly VillaDbContext _context;
        public ToursRepository(VillaDbContext context) => _context = context;

        public async Task<IEnumerable<Tour>> GetAllAsync() => await _context.Tours.ToListAsync();

        public async Task<Tour?> GetByIdAsync(int id) => await _context.Tours.FindAsync(id);

        public async Task AddAsync(Tour tour) => await _context.Tours.AddAsync(tour);

        public void Update(Tour tour) => _context.Tours.Update(tour);

        public void Delete(Tour tour) => _context.Tours.Remove(tour);

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;

        public async Task<bool> ExistsAsync(int id) => await _context.Tours.AnyAsync(e => e.Id == id);
    }
}