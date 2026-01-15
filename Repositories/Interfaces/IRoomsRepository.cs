using VillaManagementWeb.Models;

namespace VillaManagementWeb.Repositories.Interfaces
{
    public interface IRoomsRepository : IGenericRepository<Room>
    {
        Task<IEnumerable<Room>> GetActiveRoomsAsync();
        Task<IEnumerable<Room>> GetRoomsWithBookingsAsync();
    }
}

