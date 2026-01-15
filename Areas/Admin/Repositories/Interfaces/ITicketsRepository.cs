using VillaManagementWeb.Models;

namespace VillaManagementWeb.Admin.Repositories.Interfaces
{
    public interface ITicketsRepository
    {
        Task<IEnumerable<Ticket>> GetAllWithEventsAsync();
        Task<Ticket?> GetByIdWithEventAsync(int id);
        Task AddAsync(Ticket ticket);
        void Update(Ticket ticket);
        void Delete(Ticket ticket);
        Task<bool> SaveChangesAsync();
        Task<bool> ExistsAsync(int id);
    }
}