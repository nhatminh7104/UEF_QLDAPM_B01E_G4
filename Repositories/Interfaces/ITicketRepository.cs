using VillaManagementWeb.Models;

namespace VillaManagementWeb.Repositories.Interfaces
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        Task<IEnumerable<Ticket>> GetTicketsWithEventsAsync();
        Task<IEnumerable<Ticket>> GetTicketsByEventIdAsync(int eventId);
    }
}

