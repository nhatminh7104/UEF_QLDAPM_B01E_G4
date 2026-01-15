using VillaManagementWeb.Models;

namespace VillaManagementWeb.Services.Interfaces
{
    public interface IRoomBookingsService
    {
        Task<IEnumerable<RoomBooking>> GetAllRoomBookingsAsync();
        Task<RoomBooking?> GetRoomBookingByIdAsync(int id);
        Task<IEnumerable<RoomBooking>> GetRoomBookingsWithRoomsAsync();
        Task<RoomBooking> CreateRoomBookingAsync(RoomBooking RoomBooking);
        Task<RoomBooking> UpdateRoomBookingAsync(RoomBooking RoomBooking);
        Task<bool> DeleteRoomBookingAsync(int id);
        Task<bool> ValidateRoomBookingDatesAsync(DateTime checkIn, DateTime checkOut);
    }
}

