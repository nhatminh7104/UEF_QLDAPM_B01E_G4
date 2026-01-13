using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Repositories.Interfaces;
using VillaManagementWeb.Admin.Services.Interfaces;

namespace VillaManagementWeb.Admin.Services.Implementations
{
    public class ToursService : IToursService
    {
        private readonly IToursRepository _repository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ToursService(IToursRepository repository, IWebHostEnvironment hostEnvironment)
        {
            _repository = repository;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IEnumerable<Tour>> GetAllToursAsync() => await _repository.GetAllAsync();

        public async Task<Tour?> GetTourByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task CreateTourAsync(Tour tour, IFormFile? imageFile)
        {
            if (imageFile != null)
                tour.ImageUrl = await UploadImageAsync(imageFile);

            await _repository.AddAsync(tour);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateTourAsync(Tour tour, IFormFile? imageFile)
        {
            var existing = await _repository.GetByIdAsync(tour.Id);
            if (existing == null) throw new ArgumentException("Tour không tồn tại.");

            if (imageFile != null)
                tour.ImageUrl = await UploadImageAsync(imageFile);
            else
                tour.ImageUrl = existing.ImageUrl;

            _repository.Update(tour);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteTourAsync(int id)
        {
            var tour = await _repository.GetByIdAsync(id);
            if (tour == null) return false;
            _repository.Delete(tour);
            return await _repository.SaveChangesAsync();
        }

        private async Task<string> UploadImageAsync(IFormFile file)
        {
            string folder = Path.Combine(_hostEnvironment.WebRootPath, "images", "tours");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string path = Path.Combine(folder, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return "/images/tours/" + fileName;
        }
    }
}