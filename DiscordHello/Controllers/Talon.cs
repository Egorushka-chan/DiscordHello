using Microsoft.AspNetCore.Mvc;

namespace DiscordHello.Controllers
{
    public class Talon : Controller
    {
        public IActionResult Start()
        {
            return View();
        }

        public IActionResult New()
        {
            return View();
        }
    }
}
