using DiscordHello.Models;

using Microsoft.AspNetCore.Mvc;

namespace DiscordHello.Controllers
{
    public class Home : Controller
    {
        public static List<string> Talons = new List<string>(){
            "test"
        };

        public IActionResult Begin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Begin(string talon, string message)
        {
            if(Talons.Contains(talon))
            {
                string testServer = "343400746714136576";

                IDiscordSender sender = Settings.CurrentSender;
                sender.Send(testServer, message);
            }
            else
            {
                ViewBag.Error = "Талон попроси, умник";
            }

            return View();
        }
    }
}
