using Microsoft.AspNetCore.Mvc;

namespace VillaManagementWeb.Controllers
{
    public class PicturesController : Controller
    {
        // Action hiển thị trang Hình ảnh

        public IActionResult Index()
        {
            return View();
        }
        // Action hiển thị trang Video   
        public IActionResult Video()
        {
            return View();
        }        
    }
}