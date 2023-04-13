using Andrii_Mykyta_Lab3_ToP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Andrii_Mykyta_Lab3_ToP.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            var name = HttpContext.Session.GetString("Username");
            Trace.WriteLine($"{DateTime.Now:HH:mm:ss}: {name} left");
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Index", "Home");
        }
    }
}
