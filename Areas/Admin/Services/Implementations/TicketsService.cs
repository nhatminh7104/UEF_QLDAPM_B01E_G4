using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Repositories.Interfaces;
using VillaManagementWeb.Admin.Services.Interfaces;

namespace VillaManagementWeb.Admin.Services.Implementations
{
    public class TicketsService : ITicketsService
    {
        private readonly ITicketsRepository _ticketRepo;
        private readonly IEventsRepository _eventRepo;

        public TicketsService(ITicketsRepository ticketRepo, IEventsRepository eventRepo)
        {
            _ticketRepo = ticketRepo;
            _eventRepo = eventRepo;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync() => await _ticketRepo.GetAllWithEventsAsync();

        public async Task<Ticket?> GetTicketByIdAsync(int id) => await _ticketRepo.GetByIdWithEventAsync(id);

        public async Task CreateTicketAsync(Ticket ticket)
        {
            // Kiểm tra xem EventId có tồn tại không
            var eventExists = await _eventRepo.ExistsAsync(ticket.EventId);
            if (!eventExists) throw new ArgumentException("Sự kiện không tồn tại.");

            ticket.BookingDate = DateTime.Now;
            await _ticketRepo.AddAsync(ticket);
            await _ticketRepo.SaveChangesAsync();
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            if (!await _ticketRepo.ExistsAsync(ticket.Id))
                throw new ArgumentException("Vé không tồn tại.");

            _ticketRepo.Update(ticket);
            await _ticketRepo.SaveChangesAsync();
        }

        public async Task DeleteTicketAsync(int id)
        {
            var ticket = await _ticketRepo.GetByIdWithEventAsync(id);
            if (ticket != null)
            {
                _ticketRepo.Delete(ticket);
                await _ticketRepo.SaveChangesAsync();
            }
        }
    }
}