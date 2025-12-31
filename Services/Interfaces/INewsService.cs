using VillaManagementWeb.Models;

namespace VillaManagementWeb.Services.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<News>> GetAllNewsAsync();
        Task<News?> GetNewsByIdAsync(int id);
        Task<News> CreateNewsAsync(News news);
        Task<News> UpdateNewsAsync(News news);
        Task<bool> DeleteNewsAsync(int id);
    }
}

