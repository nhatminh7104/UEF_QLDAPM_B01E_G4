using Microsoft.AspNetCore.Mvc;

namespace VillaManagementWeb.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
