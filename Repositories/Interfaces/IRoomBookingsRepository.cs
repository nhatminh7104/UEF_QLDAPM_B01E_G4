using VillaManagementWeb.Models;

namespace VillaManagementWeb.Repositories.Interfaces
{
    public interface IRoomBookingsRepository : IGenericRepository<RoomBooking>
    {
        Task<IEnumerable<RoomBooking>> GetRoomBookingsWithRoomsAsync();
    }
}

