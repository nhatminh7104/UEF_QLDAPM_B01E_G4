using System.ComponentModel.DataAnnotations;

namespace VillaManagementWeb.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string PhoneNumber { get; set; }

        public string? Address { get; set; } 

        [Required(ErrorMessage = "Vui lòng để lại lời nhắn")]
        public string Message { get; set; }
    }
}