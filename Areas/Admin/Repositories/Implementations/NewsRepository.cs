using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Repositories.Interfaces;

namespace VillaManagementWeb.Admin.Repositories.Implementations
{
    public class NewsRepository : INewsRepository
    {
        private readonly VillaDbContext _context;
        public NewsRepository(VillaDbContext context) => _context = context;

        public async Task<IEnumerable<News>> GetAllAsync() =>
            await _context.News.OrderByDescending(n => n.CreatedAt).ToListAsync();

        public async Task<News?> GetByIdAsync(int id) => await _context.News.FindAsync(id);

        public async Task AddAsync(News news) => await _context.News.AddAsync(news);

        public void Update(News news) => _context.News.Update(news);

        public void Delete(News news) => _context.News.Remove(news);

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
    }
}