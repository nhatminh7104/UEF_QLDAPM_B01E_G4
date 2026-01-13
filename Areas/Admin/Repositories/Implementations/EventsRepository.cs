using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Repositories.Interfaces;

namespace VillaManagementWeb.Admin.Repositories.Implementations
{
    public class EventsRepository : IEventsRepository
    {
        private readonly VillaDbContext _context;
        public EventsRepository(VillaDbContext context) => _context = context;

        public async Task<IEnumerable<Event>> GetAllAsync() => await _context.Events.ToListAsync();

        public async Task<Event?> GetByIdAsync(int id) => await _context.Events.FindAsync(id);

        public async Task AddAsync(Event @event) => await _context.AddAsync(@event);

        public void Update(Event @event) => _context.Update(@event);

        public void Delete(Event @event) => _context.Remove(@event);

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;

        public async Task<bool> ExistsAsync(int id) => await _context.Events.AnyAsync(e => e.Id == id);
    }
}