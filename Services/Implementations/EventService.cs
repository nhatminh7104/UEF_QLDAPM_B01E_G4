using VillaManagementWeb.Models;
using VillaManagementWeb.Repositories.Interfaces;
using VillaManagementWeb.Services.Interfaces;

namespace VillaManagementWeb.Services.Implementations
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _eventRepository.GetAllAsync();
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            return await _eventRepository.GetByIdAsync(id);
        }

        public async Task<Event> CreateEventAsync(Event eventEntity)
        {
            // Business rule: Validate title is not empty
            if (string.IsNullOrWhiteSpace(eventEntity.Title))
            {
                throw new ArgumentException("Event title cannot be empty.", nameof(eventEntity));
            }

            // Business rule: Validate location is not empty
            if (string.IsNullOrWhiteSpace(eventEntity.Location))
            {
                throw new ArgumentException("Event location cannot be empty.", nameof(eventEntity));
            }

            // Business rule: Validate total tickets
            if (eventEntity.TotalTickets <= 0)
            {
                throw new ArgumentException("Total tickets must be greater than 0.", nameof(eventEntity));
            }

            var createdEvent = await _eventRepository.AddAsync(eventEntity);
            await _eventRepository.SaveAsync();
            return createdEvent;
        }

        public async Task<Event> UpdateEventAsync(Event eventEntity)
        {
            // Business rule: Validate title is not empty
            if (string.IsNullOrWhiteSpace(eventEntity.Title))
            {
                throw new ArgumentException("Event title cannot be empty.", nameof(eventEntity));
            }

            // Business rule: Validate location is not empty
            if (string.IsNullOrWhiteSpace(eventEntity.Location))
            {
                throw new ArgumentException("Event location cannot be empty.", nameof(eventEntity));
            }

            // Business rule: Validate total tickets
            if (eventEntity.TotalTickets <= 0)
            {
                throw new ArgumentException("Total tickets must be greater than 0.", nameof(eventEntity));
            }

            // Business rule: Validate event exists
            var existingEvent = await _eventRepository.GetByIdAsync(eventEntity.Id);
            if (existingEvent == null)
            {
                throw new ArgumentException($"Event with ID {eventEntity.Id} not found.", nameof(eventEntity));
            }

            _eventRepository.Update(eventEntity);
            await _eventRepository.SaveAsync();
            return eventEntity;
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(id);
            if (eventEntity == null)
            {
                return false;
            }

            _eventRepository.Delete(eventEntity);
            await _eventRepository.SaveAsync();
            return true;
        }
    }
}

