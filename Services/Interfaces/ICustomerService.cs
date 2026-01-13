using VillaManagementWeb.Models;

namespace VillaManagementWeb.Services.Interfaces
{
    public interface ICustomerService
    {
        // Hàm này rất hay dùng khi Booking:
        // Nhận vào thông tin khách -> Trả về Customer (cũ hoặc mới tạo)
        Task<Customer> GetOrCreateCustomerAsync(string fullName, string phone, string email);

        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        // Các hàm khác tùy nhu cầu quản trị...
    }
}