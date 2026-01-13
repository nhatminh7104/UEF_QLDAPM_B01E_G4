using VillaManagementWeb.Models;
using VillaManagementWeb.Repositories.Interfaces;
using VillaManagementWeb.Services.Interfaces;

namespace VillaManagementWeb.Services.Implementations
{
    public class RoomBookingService : IRoomBookingsService
    {
        private readonly IRoomBookingsRepository _RoomBookingRepository;
        private readonly IRoomsRepository _roomRepository;

        public RoomBookingService(IRoomBookingsRepository RoomBookingRepository, IRoomsRepository roomRepository)
        {
            _RoomBookingRepository = RoomBookingRepository;
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<RoomBooking>> GetAllRoomBookingsAsync()
        {
            return await _RoomBookingRepository.GetAllAsync();
        }

        public async Task<RoomBooking?> GetRoomBookingByIdAsync(int id)
        {
            return await _RoomBookingRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<RoomBooking>> GetRoomBookingsWithRoomsAsync()
        {
            return await _RoomBookingRepository.GetRoomBookingsWithRoomsAsync();
        }

        public async Task<RoomBooking> CreateRoomBookingAsync(RoomBooking RoomBooking)
        {
            // Business rule: Validate RoomBooking dates
            if (!await ValidateRoomBookingDatesAsync(RoomBooking.CheckIn, RoomBooking.CheckOut))
            {
                throw new ArgumentException("Check-out date must be later than check-in date.");
            }

            // Business rule: Validate room exists
            var room = await _roomRepository.GetByIdAsync(RoomBooking.RoomId);
            if (room == null)
            {
                throw new ArgumentException($"Room with ID {RoomBooking.RoomId} not found.", nameof(RoomBooking));
            }

            // Business rule: Validate room is active
            if (!room.IsActive)
            {
                throw new InvalidOperationException($"Room with ID {RoomBooking.RoomId} is not active.");
            }

            // Business rule: Validate total guests don't exceed room capacity
            var totalGuests = RoomBooking.AdultsCount + RoomBooking.ChildrenCount;
            if (totalGuests > room.Capacity)
            {
                throw new ArgumentException($"Total guests ({totalGuests}) exceed room capacity ({room.Capacity}).");
            }

            // Set CreatedAt if not already set
            if (RoomBooking.CreatedAt == default)
            {
                RoomBooking.CreatedAt = DateTime.Now;
            }

            var createdRoomBooking = await _RoomBookingRepository.AddAsync(RoomBooking);
            await _RoomBookingRepository.SaveAsync();
            return createdRoomBooking;
        }

        public async Task<RoomBooking> UpdateRoomBookingAsync(RoomBooking RoomBooking)
        {
            // Business rule: Validate RoomBooking dates
            if (!await ValidateRoomBookingDatesAsync(RoomBooking.CheckIn, RoomBooking.CheckOut))
            {
                throw new ArgumentException("Check-out date must be later than check-in date.");
            }

            // Business rule: Validate RoomBooking exists
            var existingRoomBooking = await _RoomBookingRepository.GetByIdAsync(RoomBooking.Id);
            if (existingRoomBooking == null)
            {
                throw new ArgumentException($"RoomBooking with ID {RoomBooking.Id} not found.", nameof(RoomBooking));
            }
            _RoomBookingRepository.Detach(existingRoomBooking);

            // Business rule: Validate room exists
            var room = await _roomRepository.GetByIdAsync(RoomBooking.RoomId);
            if (room == null)
            {
                throw new ArgumentException($"Room with ID {RoomBooking.RoomId} not found.", nameof(RoomBooking));
            }

            // Business rule: Validate room is active
            if (!room.IsActive)
            {
                throw new InvalidOperationException($"Room with ID {RoomBooking.RoomId} is not active.");
            }

            // Business rule: Validate total guests don't exceed room capacity
            var totalGuests = RoomBooking.AdultsCount + RoomBooking.ChildrenCount;
            if (totalGuests > room.Capacity)
            {
                throw new ArgumentException($"Total guests ({totalGuests}) exceed room capacity ({room.Capacity}).");
            }

            _RoomBookingRepository.Update(RoomBooking);
            await _RoomBookingRepository.SaveAsync();
            return RoomBooking;
        }

        public async Task<bool> DeleteRoomBookingAsync(int id)
        {
            var RoomBooking = await _RoomBookingRepository.GetByIdAsync(id);
            if (RoomBooking == null)
            {
                return false;
            }

            _RoomBookingRepository.Delete(RoomBooking);
            await _RoomBookingRepository.SaveAsync();
            return true;
        }

        public Task<bool> ValidateRoomBookingDatesAsync(DateTime checkIn, DateTime checkOut)
        {
            // Business rule: Check-out must be later than check-in
            var isValid = checkOut > checkIn;
            return Task.FromResult(isValid);
        }
    }
}

