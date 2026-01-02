using VillaManagementWeb.Models;

namespace VillaManagementWeb.Services.Interfaces
{
    public interface ITourService
    {
        Task<IEnumerable<Tour>> GetAllToursAsync();
        Task<Tour?> GetTourByIdAsync(int id);
        Task<Tour> CreateTourAsync(Tour tour);
        Task<Tour> UpdateTourAsync(Tour tour);
        Task<bool> DeleteTourAsync(int id);
    }
}

