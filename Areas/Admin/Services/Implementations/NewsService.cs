using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Repositories.Interfaces;
using VillaManagementWeb.Admin.Services.Interfaces;

namespace VillaManagementWeb.Admin.Services.Implementations
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _repository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public NewsService(INewsRepository repository, IWebHostEnvironment hostEnvironment)
        {
            _repository = repository;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IEnumerable<News>> GetAllNewsAsync() => await _repository.GetAllAsync();

        public async Task<News?> GetNewsByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task CreateNewsAsync(News news, IFormFile? imageFile)
        {
            if (imageFile != null)
                news.ImageUrl = await UploadImageAsync(imageFile);

            news.CreatedAt = DateTime.Now;
            await _repository.AddAsync(news);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateNewsAsync(News news, IFormFile? imageFile)
        {
            var existing = await _repository.GetByIdAsync(news.Id);
            if (existing == null) throw new ArgumentException("Tin tức không tồn tại.");

            if (imageFile != null)
                news.ImageUrl = await UploadImageAsync(imageFile);
            else
                news.ImageUrl = existing.ImageUrl;

            news.CreatedAt = existing.CreatedAt; // Giữ nguyên ngày tạo

            // Cập nhật các thay đổi
            _repository.Update(news);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteNewsAsync(int id)
        {
            var news = await _repository.GetByIdAsync(id);
            if (news == null) return false;

            _repository.Delete(news);
            return await _repository.SaveChangesAsync();
        }

        private async Task<string> UploadImageAsync(IFormFile file)
        {
            const long maxFileSize = 5 * 1024 * 1024;
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
            string ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(ext)) throw new ArgumentException("Định dạng file không hỗ trợ.");
            if (file.Length > maxFileSize) throw new ArgumentException("File quá lớn (Tối đa 5MB).");

            string folder = Path.Combine(_hostEnvironment.WebRootPath, "images", "news");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string fileName = Guid.NewGuid().ToString() + ext;
            string path = Path.Combine(folder, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return "/images/news/" + fileName;
        }
    }
}