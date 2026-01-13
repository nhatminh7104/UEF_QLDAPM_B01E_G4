using Microsoft.AspNetCore.Mvc;
using VillaManagementWeb.Models;
using VillaManagementWeb.Repositories.Interfaces;
using VillaManagementWeb.ViewModels;

namespace VillaManagementWeb.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ICustomerRepository _customerRepo;

        public ContactsController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Trả về form cũ kèm thông báo lỗi
            }

            try
            {
                // 1. Kiểm tra khách hàng đã tồn tại chưa (dựa theo SĐT)
                // Lưu ý: Repository của bạn cần có hàm tìm theo SĐT, nếu chưa có thì dùng GetAll rồi Linq
                var allCustomers = await _customerRepo.GetAllAsync();
                var existingCustomer = allCustomers.FirstOrDefault(c => c.PhoneNumber == model.PhoneNumber);

                string timeStamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                string noteContent = $"{model.Message}";

                if (existingCustomer == null)
                {
                    // --- KHÁCH MỚI: TẠO MỚI ---
                    var newCustomer = new Customer
                    {
                        FullName = model.FullName,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        Address = model.Address,
                        Note = noteContent, // Lưu lời nhắn vào Note
                        CreatedAt = DateTime.Now
                    };
                    await _customerRepo.AddAsync(newCustomer);
                }
                else
                {
                    // --- KHÁCH CŨ: CẬP NHẬT ---
                    existingCustomer.FullName = model.FullName; // Cập nhật tên nếu muốn
                    existingCustomer.Email = model.Email;       // Cập nhật email mới nhất
                    if (!string.IsNullOrEmpty(model.Address))
                    {
                        existingCustomer.Address = model.Address;
                    }

                    // Cộng dồn tin nhắn mới vào tin nhắn cũ
                    if (string.IsNullOrEmpty(existingCustomer.Note))
                    {
                        existingCustomer.Note = noteContent;
                    }
                    else
                    {
                        existingCustomer.Note += "\n" + noteContent;
                    }

                    await _customerRepo.UpdateAsync(existingCustomer);
                }

                // 2. Chuyển hướng sang trang Success
                return RedirectToAction("Success");
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                ModelState.AddModelError("", "Đã có lỗi xảy ra, vui lòng thử lại sau.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
    }
}