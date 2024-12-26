using Microsoft.AspNetCore.Mvc;

namespace Hairr.Controllers
{
    public class ImageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
