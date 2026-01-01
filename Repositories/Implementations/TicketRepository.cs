using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.Repositories.Interfaces;

namespace VillaManagementWeb.Repositories.Implementations
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(VillaDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Ticket>> GetTicketsWithEventsAsync()
        {
            return await _dbSet
                .Include(t => t.Event)
                .OrderByDescending(t => t.BookingDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByEventIdAsync(int eventId)
        {
            return await _dbSet
                .Include(t => t.Event)
                .Where(t => t.EventId == eventId)
                .OrderByDescending(t => t.BookingDate)
                .ToListAsync();
        }
    }
}

