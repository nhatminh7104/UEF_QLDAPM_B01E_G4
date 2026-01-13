using VillaManagementWeb.Models;

namespace VillaManagementWeb.Admin.Services.Interfaces
{
    public interface IToursService
    {
        Task<IEnumerable<Tour>> GetAllToursAsync();
        Task<Tour?> GetTourByIdAsync(int id);
        Task CreateTourAsync(Tour tour, IFormFile? imageFile);
        Task UpdateTourAsync(Tour tour, IFormFile? imageFile);
        Task<bool> DeleteTourAsync(int id);
    }
}