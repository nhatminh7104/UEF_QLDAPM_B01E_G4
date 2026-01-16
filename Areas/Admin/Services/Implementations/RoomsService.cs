using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Repositories.Interfaces;
using VillaManagementWeb.Admin.Services.Interfaces;

namespace VillaManagementWeb.Admin.Services.Implementations
{
    public class RoomsService : IRoomsService
    {
        private readonly IRoomsRepository _repository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public RoomsService(IRoomsRepository repository, IWebHostEnvironment hostEnvironment)
        {
            _repository = repository;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<IEnumerable<Room>> GetAllRoomsAsync() => await _repository.GetAllRoomsAsync();
        public async Task<IEnumerable<Room>> GetRoomsWithStatusAsync() => await _repository.GetAllRoomsAsync();

        public async Task<Room?> GetRoomByIdAsync(int id) => await _repository.GetRoomByIdAsync(id);

        public async Task<bool> CreateRoomAsync(Room room, List<IFormFile> imageFiles)
        {
            if (imageFiles != null && imageFiles.Count > 0)
            {
                room.ImageUrl = await UploadFileAsync(imageFiles[0]);
                room.RoomImages = new List<RoomImage>();
                for (int i = 1; i < imageFiles.Count; i++)
                {
                    room.RoomImages.Add(new RoomImage
                    {
                        ImageUrl = await UploadFileAsync(imageFiles[i]),
                        Room = room
                    });
                }
            }
            await _repository.AddAsync(room);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> UpdateRoomAsync(Room room, List<IFormFile> imageFiles)
        {
            var existing = await _repository.GetRoomByIdAsync(room.Id);
            if (existing == null) return false;

            if (imageFiles != null && imageFiles.Count > 0)
                room.ImageUrl = await UploadFileAsync(imageFiles[0]);
            else
                room.ImageUrl = existing.ImageUrl;

            _repository.Update(room);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            var room = await _repository.GetRoomByIdAsync(id);
            if (room == null) return false;
            _repository.Delete(room);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> RoomExistsAsync(int id) => await _repository.ExistsAsync(id);

        private async Task<string> UploadFileAsync(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string roomPath = Path.Combine(_hostEnvironment.WebRootPath, "images", "rooms");
            if (!Directory.Exists(roomPath)) Directory.CreateDirectory(roomPath);

            string fullPath = Path.Combine(roomPath, fileName);
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return "/images/rooms/" + fileName;
        }
    }
}