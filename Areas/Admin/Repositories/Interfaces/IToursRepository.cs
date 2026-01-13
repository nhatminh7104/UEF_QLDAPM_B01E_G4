using VillaManagementWeb.Models;

namespace VillaManagementWeb.Admin.Repositories.Interfaces
{
    public interface IToursRepository
    {
        Task<IEnumerable<Tour>> GetAllAsync();
        Task<Tour?> GetByIdAsync(int id);
        Task AddAsync(Tour tour);
        void Update(Tour tour);
        void Delete(Tour tour);
        Task<bool> SaveChangesAsync();
        Task<bool> ExistsAsync(int id);
    }
}