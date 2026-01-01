using VillaManagementWeb.Models;
using VillaManagementWeb.ViewModels;

namespace VillaManagementWeb.Services.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<Room?> GetRoomByIdAsync(int id);
        Task<IEnumerable<Room>> GetActiveRoomsAsync();
        Task<Room> CreateRoomAsync(Room room);
        Task<Room> UpdateRoomAsync(Room room);
        Task<bool> DeleteRoomAsync(int id);
        Task<bool> ValidateRoomCapacityAsync(int roomId, int numberOfGuests);
        Task<decimal> CalculatePriceAsync(int roomId, DateTime checkIn, DateTime checkOut);
        Task<IEnumerable<RoomIndexVM>> GetRoomsWithStatusAsync();
    }
}

