using VillaManagementWeb.Models;

namespace VillaManagementWeb.Repositories.Interfaces
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        Task<IEnumerable<Room>> GetActiveRoomsAsync();
        Task<IEnumerable<Room>> GetRoomsWithBookingsAsync();
    }
}

