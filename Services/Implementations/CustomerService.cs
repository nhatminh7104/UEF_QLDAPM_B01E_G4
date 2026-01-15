using VillaManagementWeb.Models;
using VillaManagementWeb.Repositories.Interfaces;
using VillaManagementWeb.Services.Interfaces;

namespace VillaManagementWeb.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerService(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<Customer> GetOrCreateCustomerAsync(string fullName, string phone, string email)
        {
            // 1. Tìm xem khách có trong DB chưa (theo SĐT)
            var existingCustomer = await _customerRepo.GetByPhoneNumberAsync(phone);

            if (existingCustomer != null)
            {
                // Nếu có rồi, có thể update lại Tên hoặc Email nếu khách thay đổi (tùy logic)
                return existingCustomer;
            }

            // 2. Nếu chưa có, tạo mới
            var newCustomer = new Customer
            {
                FullName = fullName,
                PhoneNumber = phone,
                Email = email,
                CreatedAt = DateTime.Now
            };

            await _customerRepo.AddAsync(newCustomer);
            return newCustomer;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepo.GetAllAsync();
        }
    }
}