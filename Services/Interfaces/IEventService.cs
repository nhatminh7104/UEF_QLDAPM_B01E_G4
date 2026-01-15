using VillaManagementWeb.Models;

namespace VillaManagementWeb.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event?> GetEventByIdAsync(int id);
        Task<Event> CreateEventAsync(Event eventEntity);
        Task<Event> UpdateEventAsync(Event eventEntity);
        Task<bool> DeleteEventAsync(int id);
    }
}

