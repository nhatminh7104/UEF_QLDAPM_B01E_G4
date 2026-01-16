using VillaManagementWeb.Models;
using VillaManagementWeb.Repositories.Implementations;
using VillaManagementWeb.Repositories.Interfaces;
using VillaManagementWeb.Services.Interfaces;

namespace VillaManagementWeb.Services.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IEventsRepository _eventRepository;

        public TicketService(ITicketRepository ticketRepository, IEventsRepository eventRepository)
        {
            _ticketRepository = ticketRepository;
            _eventRepository = eventRepository; // Bây giờ hai kiểu đã khớp nhau
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _ticketRepository.GetAllAsync();
        }

        public async Task<Ticket?> GetTicketByIdAsync(int id)
        {
            return await _ticketRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsWithEventsAsync()
        {
            return await _ticketRepository.GetTicketsWithEventsAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByEventIdAsync(int eventId)
        {
            return await _ticketRepository.GetTicketsByEventIdAsync(eventId);
        }

        public async Task<Ticket> CreateTicketAsync(Ticket ticket)
        {
            // Business rule: Validate customer name is not empty
            if (string.IsNullOrWhiteSpace(ticket.CustomerName))
            {
                throw new ArgumentException("Customer name cannot be empty.", nameof(ticket));
            }

            // Business rule: Validate customer email is not empty
            if (string.IsNullOrWhiteSpace(ticket.CustomerEmail))
            {
                throw new ArgumentException("Customer email cannot be empty.", nameof(ticket));
            }

            // Business rule: Validate ticket type is not empty
            if (string.IsNullOrWhiteSpace(ticket.TicketType))
            {
                throw new ArgumentException("Ticket type cannot be empty.", nameof(ticket));
            }

            // Business rule: Validate quantity
            if (ticket.Quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than 0.", nameof(ticket));
            }

            // Business rule: Validate price
            if (ticket.Price < 0)
            {
                throw new ArgumentException("Price cannot be negative.", nameof(ticket));
            }

            // Business rule: Validate event exists
            var eventEntity = await _eventRepository.GetByIdAsync(ticket.EventId);
            if (eventEntity == null)
            {
                throw new ArgumentException($"Event with ID {ticket.EventId} not found.", nameof(ticket));
            }

            // Set BookingDate if not already set
            if (ticket.BookingDate == default)
            {
                ticket.BookingDate = DateTime.Now;
            }

            // Generate QRCode if not provided
            if (string.IsNullOrWhiteSpace(ticket.QRCode))
            {
                ticket.QRCode = Guid.NewGuid().ToString();
            }

            var createdTicket = await _ticketRepository.AddAsync(ticket);
            await _ticketRepository.SaveAsync();
            return createdTicket;
        }

        public async Task<Ticket> UpdateTicketAsync(Ticket ticket)
        {
            // Business rule: Validate customer name is not empty
            if (string.IsNullOrWhiteSpace(ticket.CustomerName))
            {
                throw new ArgumentException("Customer name cannot be empty.", nameof(ticket));
            }

            // Business rule: Validate customer email is not empty
            if (string.IsNullOrWhiteSpace(ticket.CustomerEmail))
            {
                throw new ArgumentException("Customer email cannot be empty.", nameof(ticket));
            }

            // Business rule: Validate ticket type is not empty
            if (string.IsNullOrWhiteSpace(ticket.TicketType))
            {
                throw new ArgumentException("Ticket type cannot be empty.", nameof(ticket));
            }

            // Business rule: Validate quantity
            if (ticket.Quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than 0.", nameof(ticket));
            }

            // Business rule: Validate price
            if (ticket.Price < 0)
            {
                throw new ArgumentException("Price cannot be negative.", nameof(ticket));
            }

            // Business rule: Validate ticket exists
            var existingTicket = await _ticketRepository.GetByIdAsync(ticket.Id);
            if (existingTicket == null)
            {
                throw new ArgumentException($"Ticket with ID {ticket.Id} not found.", nameof(ticket));
            }

            _ticketRepository.Update(ticket);
            await _ticketRepository.SaveAsync();
            return ticket;
        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
            {
                return false;
            }

            _ticketRepository.Delete(ticket);
            await _ticketRepository.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<Ticket>> GenerateTicketsAsync(int eventId, int quantity)
        {
            // Business rule: Validate event exists
            var eventEntity = await _eventRepository.GetByIdAsync(eventId);
            if (eventEntity == null)
            {
                throw new ArgumentException($"Event with ID {eventId} not found.", nameof(eventId));
            }

            // Business rule: Validate quantity
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than 0.", nameof(quantity));
            }

            var tickets = new List<Ticket>();

            for (int i = 0; i < quantity; i++)
            {
                var ticket = new Ticket
                {
                    EventId = eventId,
                    QRCode = Guid.NewGuid().ToString(),
                    BookingDate = DateTime.Now,
                    IsUsed = false,
                    TicketType = "Standard", // Default ticket type
                    Price = 0, // Default price, should be set when selling
                    CustomerName = "", // Empty, to be filled when sold
                    CustomerEmail = "", // Empty, to be filled when sold
                    Quantity = 1 // Each generated ticket represents one ticket
                };

                await _ticketRepository.AddAsync(ticket);
                tickets.Add(ticket);
            }

            await _ticketRepository.SaveAsync();
            return tickets;
        }
    }
}

