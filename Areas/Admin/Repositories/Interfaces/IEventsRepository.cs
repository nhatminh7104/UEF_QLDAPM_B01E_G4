using VillaManagementWeb.Models;

namespace VillaManagementWeb.Admin.Repositories.Interfaces
{
    public interface IEventsRepository
    {
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(int id);
        Task AddAsync(Event @event);
        void Update(Event @event);
        void Delete(Event @event);
        Task<bool> SaveChangesAsync();
        Task<bool> ExistsAsync(int id);
    }
}