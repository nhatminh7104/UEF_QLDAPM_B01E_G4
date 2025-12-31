using VillaManagementWeb.Models;
using VillaManagementWeb.Repositories.Interfaces;
using VillaManagementWeb.Services.Interfaces;

namespace VillaManagementWeb.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _bookingRepository.GetAllAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            return await _bookingRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Booking>> GetBookingsWithRoomsAsync()
        {
            return await _bookingRepository.GetBookingsWithRoomsAsync();
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            // Business rule: Validate booking dates
            if (!await ValidateBookingDatesAsync(booking.CheckIn, booking.CheckOut))
            {
                throw new ArgumentException("Check-out date must be later than check-in date.");
            }

            // Business rule: Validate room exists
            var room = await _roomRepository.GetByIdAsync(booking.RoomId);
            if (room == null)
            {
                throw new ArgumentException($"Room with ID {booking.RoomId} not found.", nameof(booking));
            }

            // Business rule: Validate room is active
            if (!room.IsActive)
            {
                throw new InvalidOperationException($"Room with ID {booking.RoomId} is not active.");
            }

            // Business rule: Validate total guests don't exceed room capacity
            var totalGuests = booking.AdultsCount + booking.ChildrenCount;
            if (totalGuests > room.Capacity)
            {
                throw new ArgumentException($"Total guests ({totalGuests}) exceed room capacity ({room.Capacity}).");
            }

            // Set CreatedAt if not already set
            if (booking.CreatedAt == default)
            {
                booking.CreatedAt = DateTime.Now;
            }

            var createdBooking = await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveAsync();
            return createdBooking;
        }

        public async Task<Booking> UpdateBookingAsync(Booking booking)
        {
            // Business rule: Validate booking dates
            if (!await ValidateBookingDatesAsync(booking.CheckIn, booking.CheckOut))
            {
                throw new ArgumentException("Check-out date must be later than check-in date.");
            }

            // Business rule: Validate booking exists
            var existingBooking = await _bookingRepository.GetByIdAsync(booking.Id);
            if (existingBooking == null)
            {
                throw new ArgumentException($"Booking with ID {booking.Id} not found.", nameof(booking));
            }
            _bookingRepository.Detach(existingBooking);

            // Business rule: Validate room exists
            var room = await _roomRepository.GetByIdAsync(booking.RoomId);
            if (room == null)
            {
                throw new ArgumentException($"Room with ID {booking.RoomId} not found.", nameof(booking));
            }

            // Business rule: Validate room is active
            if (!room.IsActive)
            {
                throw new InvalidOperationException($"Room with ID {booking.RoomId} is not active.");
            }

            // Business rule: Validate total guests don't exceed room capacity
            var totalGuests = booking.AdultsCount + booking.ChildrenCount;
            if (totalGuests > room.Capacity)
            {
                throw new ArgumentException($"Total guests ({totalGuests}) exceed room capacity ({room.Capacity}).");
            }

            _bookingRepository.Update(booking);
            await _bookingRepository.SaveAsync();
            return booking;
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                return false;
            }

            _bookingRepository.Delete(booking);
            await _bookingRepository.SaveAsync();
            return true;
        }

        public Task<bool> ValidateBookingDatesAsync(DateTime checkIn, DateTime checkOut)
        {
            // Business rule: Check-out must be later than check-in
            var isValid = checkOut > checkIn;
            return Task.FromResult(isValid);
        }
    }
}

