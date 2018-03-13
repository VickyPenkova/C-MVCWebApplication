using Microsoft.AspNetCore.Mvc;

namespace MVC_Client.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}