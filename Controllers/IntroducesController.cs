using Microsoft.AspNetCore.Mvc;

namespace VillaManagementWeb.Controllers
{
    public class IntroducesController : Controller
    {
        [Route("About")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
