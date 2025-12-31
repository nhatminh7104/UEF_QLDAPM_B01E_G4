using VillaManagementWeb.Models;
using VillaManagementWeb.Repositories.Interfaces;
using VillaManagementWeb.Services.Interfaces;

namespace VillaManagementWeb.Services.Implementations
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<IEnumerable<News>> GetAllNewsAsync()
        {
            return await _newsRepository.GetAllAsync();
        }

        public async Task<News?> GetNewsByIdAsync(int id)
        {
            return await _newsRepository.GetByIdAsync(id);
        }

        public async Task<News> CreateNewsAsync(News news)
        {
            // Business rule: Validate title is not empty
            if (string.IsNullOrWhiteSpace(news.Title))
            {
                throw new ArgumentException("News title cannot be empty.", nameof(news));
            }

            // Set CreatedAt if not already set
            if (news.CreatedAt == default)
            {
                news.CreatedAt = DateTime.Now;
            }

            var createdNews = await _newsRepository.AddAsync(news);
            await _newsRepository.SaveAsync();
            return createdNews;
        }

        public async Task<News> UpdateNewsAsync(News news)
        {
            // Business rule: Validate title is not empty
            if (string.IsNullOrWhiteSpace(news.Title))
            {
                throw new ArgumentException("News title cannot be empty.", nameof(news));
            }

            // Business rule: Validate news exists
            var existingNews = await _newsRepository.GetByIdAsync(news.Id);
            if (existingNews == null)
            {
                throw new ArgumentException($"News with ID {news.Id} not found.", nameof(news));
            }

            _newsRepository.Update(news);
            await _newsRepository.SaveAsync();
            return news;
        }

        public async Task<bool> DeleteNewsAsync(int id)
        {
            var news = await _newsRepository.GetByIdAsync(id);
            if (news == null)
            {
                return false;
            }

            _newsRepository.Delete(news);
            await _newsRepository.SaveAsync();
            return true;
        }
    }
}

