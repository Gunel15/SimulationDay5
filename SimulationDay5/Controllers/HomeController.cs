using Microsoft.AspNetCore.Mvc;

namespace SimulationDay5.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
