using Microsoft.AspNetCore.Mvc;

namespace VillaManagementWeb.Controllers
{
    public class PicturesController : Controller
    {
        // Action hiển thị trang Video
        public IActionResult Video()
        {
            return View();
        }

        // Action hiển thị trang Hình ảnh
        public IActionResult Image()
        {
            return View();
        }
    }
}