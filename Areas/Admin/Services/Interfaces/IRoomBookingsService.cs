using VillaManagementWeb.Models;

namespace VillaManagementWeb.Admin.Services.Interfaces
{
    public interface IRoomBookingsService
    {
        Task<IEnumerable<RoomBooking>> GetRoomBookingsWithRoomsAsync();
        Task<RoomBooking?> GetRoomBookingByIdAsync(int id);
        Task CreateRoomBookingAsync(RoomBooking RoomBooking);
        Task UpdateRoomBookingAsync(RoomBooking RoomBooking);
        Task DeleteRoomBookingAsync(int id);
    }
}