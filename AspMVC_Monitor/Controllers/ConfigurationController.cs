using Microsoft.AspNetCore.Mvc;

namespace AspMVC_Monitor.Controllers
{
    public class ConfigurationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
