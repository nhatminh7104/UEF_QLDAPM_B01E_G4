using VillaManagementWeb.Models;

namespace VillaManagementWeb.Admin.Services.Interfaces
{
    public interface IEventsService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event?> GetEventByIdAsync(int id);
        Task<bool> CreateEventAsync(Event @event);
        Task<bool> UpdateEventAsync(Event @event);
        Task<bool> DeleteEventAsync(int id);
    }
}