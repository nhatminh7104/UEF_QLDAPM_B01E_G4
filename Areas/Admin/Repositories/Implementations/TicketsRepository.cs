using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Repositories.Interfaces;

namespace VillaManagementWeb.Admin.Repositories.Implementations
{
    public class TicketsRepository : ITicketsRepository
    {
        private readonly VillaDbContext _context;
        public TicketsRepository(VillaDbContext context) => _context = context;

        public async Task<IEnumerable<Ticket>> GetAllWithEventsAsync() =>
            await _context.Tickets.Include(t => t.Event).ToListAsync();

        public async Task<Ticket?> GetByIdWithEventAsync(int id) =>
            await _context.Tickets.Include(t => t.Event).FirstOrDefaultAsync(m => m.Id == id);

        public async Task AddAsync(Ticket ticket) => await _context.Tickets.AddAsync(ticket);

        public void Update(Ticket ticket) => _context.Tickets.Update(ticket);

        public void Delete(Ticket ticket) => _context.Tickets.Remove(ticket);

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;

        public async Task<bool> ExistsAsync(int id) => await _context.Tickets.AnyAsync(e => e.Id == id);
    }
}