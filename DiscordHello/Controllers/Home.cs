using System.Diagnostics.Eventing.Reader;
using System.Reflection;

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
        public IActionResult Begin(string talon, string message,[FromServices] IEnumerable<IDiscordSender> discordSenders)
        {
            if(Talons.Contains(talon))
            {
                string testServer = "343400746714136576";

                foreach(IDiscordSender discordSender in discordSenders)
                {
                    if (discordSender is FileBetterDiscordSender)
                    {
                        bool success = false;
                        try
                        {
                            discordSender.Send(testServer, message);
                            success = true;
                        }
                        catch (FileNotFoundException)
                        {
                            ViewBag.Error = "Попроси админа настроить буфер";
                        }
                        catch (ArgumentException)
                        {
                            ViewBag.Error = "Ты просил отправить пустое сообщение?";
                        }
                        finally
                        {
                            if (success)
                            {
                                ViewBag.Message = "Запрос выполнен успешно";
                                ModelState.Clear();
                            }
                        }
                        
                    }
                }
            }
            else
            {
                ViewBag.Error = "Талон попроси, умник";
            }

            return View();
        }
    }
}
