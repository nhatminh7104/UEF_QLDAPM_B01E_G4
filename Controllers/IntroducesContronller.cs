using Microsoft.AspNetCore.Mvc;

namespace VillaManagementWeb.Controllers
{
    public class IntroducesContronller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
