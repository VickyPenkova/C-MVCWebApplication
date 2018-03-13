using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC_Client.Models;

namespace MVC_Client.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Customers()
        {
            ViewData["Message"] = "Customers Information";
            return Redirect("/customers");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
