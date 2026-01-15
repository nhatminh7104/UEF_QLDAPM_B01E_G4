using VillaManagementWeb.Models;

namespace VillaManagementWeb.Admin.Services.Interfaces
{
    public interface ITicketsService
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<Ticket?> GetTicketByIdAsync(int id);
        Task CreateTicketAsync(Ticket ticket);
        Task UpdateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(int id);
    }
}