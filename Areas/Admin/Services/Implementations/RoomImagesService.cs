using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Repositories.Interfaces;
using VillaManagementWeb.Admin.Services.Interfaces;

namespace VillaManagementWeb.Admin.Services.Implementations
{
    public class RoomImagesService : IRoomImagesService
    {
        private readonly IRoomImagesRepository _repository;

        public RoomImagesService(IRoomImagesRepository repository) => _repository = repository;

        public async Task<IEnumerable<RoomImage>> GetAllImagesAsync() => await _repository.GetAllWithRoomAsync();

        public async Task<RoomImage?> GetImageByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task CreateImageAsync(RoomImage roomImage)
        {
            await _repository.AddAsync(roomImage);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateImageAsync(RoomImage roomImage)
        {
            _repository.Update(roomImage);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteImageAsync(int id)
        {
            var image = await _repository.GetByIdAsync(id);
            if (image != null)
            {
                _repository.Delete(image);
                await _repository.SaveChangesAsync();
            }
        }
    }
}