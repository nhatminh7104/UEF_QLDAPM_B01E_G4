using VillaManagementWeb.Models;

namespace VillaManagementWeb.Admin.Services.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<News>> GetAllNewsAsync();
        Task<News?> GetNewsByIdAsync(int id);
        Task CreateNewsAsync(News news, IFormFile? imageFile);
        Task UpdateNewsAsync(News news, IFormFile? imageFile);
        Task<bool> DeleteNewsAsync(int id);
    }
}