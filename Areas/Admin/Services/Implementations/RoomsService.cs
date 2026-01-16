using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Repositories.Interfaces;
using VillaManagementWeb.Admin.Services.Interfaces;
using Microsoft.EntityFrameworkCore; // Thêm nếu cần dùng các hàm mở rộng

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

        // --- BỔ SUNG: Lấy phòng kèm album ảnh ---
        public async Task<Room?> GetRoomWithImagesAsync(int id)
        {
            return await _repository.GetRoomWithImagesAsync(id);
        }

        // --- BỔ SUNG: Lấy danh sách danh mục để đổ vào Dropdown ---
        public async Task<IEnumerable<RoomCategory>> GetRoomCategoriesAsync()
        {
            return await _repository.GetRoomCategoriesAsync();
        }

        public async Task<bool> CreateRoomAsync(Room room, List<IFormFile> imageFiles)
        {
            if (imageFiles != null && imageFiles.Count > 0)
            {
                // Ảnh đầu tiên làm ảnh đại diện (ImageUrl)
                room.ImageUrl = await UploadFileAsync(imageFiles[0]);

                // Các ảnh từ vị trí thứ 2 trở đi đưa vào album (RoomImages)
                room.RoomImages = new List<RoomImage>();
                for (int i = 1; i < imageFiles.Count; i++)
                {
                    room.RoomImages.Add(new RoomImage
                    {
                        ImageUrl = await UploadFileAsync(imageFiles[i])
                    });
                }
            }
            await _repository.AddAsync(room);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> UpdateRoomAsync(Room room, List<IFormFile> imageFiles)
        {
            // Không nên dùng GetRoomByIdAsync ở đây để tránh lỗi Tracking của EF Core
            // Chúng ta sẽ cập nhật các trường cụ thể hoặc dùng kỹ thuật Attach

            if (imageFiles != null && imageFiles.Count > 0)
            {
                // Nếu có ảnh mới, cập nhật ảnh đại diện
                room.ImageUrl = await UploadFileAsync(imageFiles[0]);

                // Nếu muốn thêm các ảnh còn lại vào Album cũ
                if (room.RoomImages == null) room.RoomImages = new List<RoomImage>();
                for (int i = 1; i < imageFiles.Count; i++)
                {
                    room.RoomImages.Add(new RoomImage
                    {
                        ImageUrl = await UploadFileAsync(imageFiles[i]),
                        RoomId = room.Id
                    });
                }
            }

            _repository.Update(room);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            var room = await _repository.GetRoomByIdAsync(id);
            if (room == null) return false;

            // Lưu ý: Bạn có thể thêm logic xóa file vật lý trong thư mục tại đây trước khi xóa DB
            _repository.Delete(room);
            return await _repository.SaveChangesAsync();
        }

        // --- BỔ SUNG: Xóa một ảnh lẻ (AJAX) ---
        public async Task<bool> DeleteRoomImageAsync(int imageId)
        {
            var image = await _repository.GetRoomImageByIdAsync(imageId);
            if (image == null) return false;

            // 1. Xóa file vật lý trên server
            DeletePhysicalFile(image.ImageUrl);

            // 2. Xóa trong DB
            _repository.DeleteImage(image);
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

        // --- BỔ SUNG: Hàm hỗ trợ xóa file vật lý ---
        private void DeletePhysicalFile(string? relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return;

            var fullPath = Path.Combine(_hostEnvironment.WebRootPath, relativePath.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}