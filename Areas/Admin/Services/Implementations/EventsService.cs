using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Repositories.Interfaces;
using VillaManagementWeb.Admin.Services.Interfaces;

namespace VillaManagementWeb.Admin.Services.Implementations
{
    public class EventsService : IEventsService
    {
        private readonly IEventsRepository _repository;

        public EventsService(IEventsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync() => await _repository.GetAllAsync();

        public async Task<Event?> GetEventByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<bool> CreateEventAsync(Event @event)
        {
            await _repository.AddAsync(@event);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> UpdateEventAsync(Event @event)
        {
            _repository.Update(@event);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var @event = await _repository.GetByIdAsync(id);
            if (@event == null) return false;
            _repository.Delete(@event);
            return await _repository.SaveChangesAsync();
        }
    }
}