using VillaManagementWeb.Models;
using VillaManagementWeb.Repositories.Interfaces;
using VillaManagementWeb.Services.Interfaces;
using VillaManagementWeb.ViewModels;

namespace VillaManagementWeb.Services.Implementations
{
    public class RoomsService : IRoomsService
    {
        private readonly IRoomsRepository _roomRepository;

        public RoomsService(IRoomsRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _roomRepository.GetAllAsync();
        }

        public async Task<Room?> GetRoomByIdAsync(int id)
        {
            return await _roomRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Room>> GetActiveRoomsAsync()
        {
            return await _roomRepository.GetActiveRoomsAsync();
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {
            // Business rule: Validate room capacity
            if (room.Capacity <= 0)
            {
                throw new ArgumentException("Room capacity must be greater than 0.", nameof(room));
            }

            // Business rule: Validate pricing
            if (room.PricePerNight < 0)
            {
                throw new ArgumentException("Room price per night cannot be negative.", nameof(room));
            }

            // Business rule: Validate rating stars
            if (room.RatingStars < 0 || room.RatingStars > 5)
            {
                throw new ArgumentException("Rating stars must be between 0 and 5.", nameof(room));
            }

            var createdRoom = await _roomRepository.AddAsync(room);
            await _roomRepository.SaveAsync();
            return createdRoom;
        }

        public async Task<Room> UpdateRoomAsync(Room room)
        {
            // Business rule: Validate room capacity
            if (room.Capacity <= 0)
            {
                throw new ArgumentException("Room capacity must be greater than 0.", nameof(room));
            }

            // Business rule: Validate pricing
            if (room.PricePerNight < 0)
            {
                throw new ArgumentException("Room price per night cannot be negative.", nameof(room));
            }

            // Business rule: Validate rating stars
            if (room.RatingStars < 0 || room.RatingStars > 5)
            {
                throw new ArgumentException("Rating stars must be between 0 and 5.", nameof(room));
            }

            _roomRepository.Update(room);
            await _roomRepository.SaveAsync();
            return room;
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
            {
                return false;
            }

            _roomRepository.Delete(room);
            await _roomRepository.SaveAsync();
            return true;
        }

        public async Task<bool> ValidateRoomCapacityAsync(int roomId, int numberOfGuests)
        {
            var room = await _roomRepository.GetByIdAsync(roomId);
            if (room == null)
            {
                return false;
            }

            return numberOfGuests > 0 && numberOfGuests <= room.Capacity;
        }

        public async Task<decimal> CalculatePriceAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            // Business rule: Validate dates
            if (checkOut <= checkIn)
            {
                throw new ArgumentException("Check-out date must be later than check-in date.");
            }

            var room = await _roomRepository.GetByIdAsync(roomId);
            if (room == null)
            {
                throw new ArgumentException($"Room with ID {roomId} not found.", nameof(roomId));
            }

            // Calculate number of nights
            var numberOfNights = (checkOut - checkIn).Days;

            // Business rule: Minimum 1 night stay
            if (numberOfNights < 1)
            {
                numberOfNights = 1;
            }

            // Calculate total price
            var totalPrice = room.PricePerNight * numberOfNights;

            return totalPrice;
        }

        public async Task<IEnumerable<RoomIndexVM>> GetRoomsWithStatusAsync()
        {
            var rooms = await _roomRepository.GetRoomsWithBookingsAsync();
            var now = DateTime.Now;
            var result = new List<RoomIndexVM>();

            foreach (var room in rooms)
            {
                var vm = new RoomIndexVM
                {
                    Id = room.Id,
                    RoomNumber = room.RoomNumber,
                    Type = room.Type,
                    PricePerNight = room.PricePerNight,
                    Capacity = room.Capacity,
                    RatingStars = room.RatingStars,
                    IsActive = room.IsActive,
                    Description = room.Description,
                    ImageUrl = room.ImageUrl,
                    SquareFootage = room.SquareFootage,
                    HasWifi = room.HasWifi,
                    HasBreakfast = room.HasBreakfast,
                    HasPool = room.HasPool,
                    HasTowel = room.HasTowel
                };

                // Determine rental status based on bookings
                var confirmedBookings = room.RoomBookings?.Where(b => b.Status == "Confirmed") ?? Enumerable.Empty<RoomBooking>();

                // Check if room is currently occupied
                bool isOccupied = confirmedBookings.Any(b =>
                    b.CheckIn <= now && now <= b.CheckOut);

                if (isOccupied)
                {
                    vm.RentalStatus = "Occupied";
                    vm.StatusLabel = "Đang cho thuê";
                    vm.StatusColor = "badge-danger";
                }
                else
                {
                    // Check if room has future reservations
                    bool hasReservation = confirmedBookings.Any(b => b.CheckIn > now);

                    if (hasReservation)
                    {
                        vm.RentalStatus = "Reserved";
                        vm.StatusLabel = "Đã đặt";
                        vm.StatusColor = "badge-info";
                    }
                    else
                    {
                        vm.RentalStatus = "Available";
                        vm.StatusLabel = "Trống";
                        vm.StatusColor = "badge-success";
                    }
                }

                result.Add(vm);
            }

            return result;
        }
    }
}

