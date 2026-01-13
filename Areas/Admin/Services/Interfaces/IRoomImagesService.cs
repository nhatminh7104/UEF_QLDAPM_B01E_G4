using VillaManagementWeb.Models;

namespace VillaManagementWeb.Admin.Services.Interfaces
{
    public interface IRoomImagesService
    {
        Task<IEnumerable<RoomImage>> GetAllImagesAsync();
        Task<RoomImage?> GetImageByIdAsync(int id);
        Task CreateImageAsync(RoomImage roomImage);
        Task UpdateImageAsync(RoomImage roomImage);
        Task DeleteImageAsync(int id);
    }
}