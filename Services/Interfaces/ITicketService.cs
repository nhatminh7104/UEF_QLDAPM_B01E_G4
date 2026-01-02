using VillaManagementWeb.Models;

namespace VillaManagementWeb.Services.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<Ticket?> GetTicketByIdAsync(int id);
        Task<IEnumerable<Ticket>> GetTicketsWithEventsAsync();
        Task<IEnumerable<Ticket>> GetTicketsByEventIdAsync(int eventId);
        Task<Ticket> CreateTicketAsync(Ticket ticket);
        Task<Ticket> UpdateTicketAsync(Ticket ticket);
        Task<bool> DeleteTicketAsync(int id);
        Task<IEnumerable<Ticket>> GenerateTicketsAsync(int eventId, int quantity);
    }
}

