using AspMVC_Monitor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;

namespace AspMVC_Monitor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAssetsMonitor _assetsMonitor;

        public HomeController(ILogger<HomeController> logger, IAssetsMonitor assetsMonitor)
        {
            this._logger = logger;
            this._assetsMonitor = assetsMonitor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Monitor()
        {
            HttpContext.Session.SetString("Time", DateTime.Now.ToString());
            return View(_assetsMonitor.AssetList);
        }

        [HttpPost]
        public IActionResult GetServerTime()
        {
            HttpContext.Session.SetString("Time", DateTime.Now.ToString());
            return Json(new { data = HttpContext.Session.GetString("Time") });
        }

        [HttpPost]
        public IActionResult GetAssetList()
        {
            var itemList = _assetsMonitor.AssetList.Select((a => new { 
                name = a.NameUI, 
                ipAddress = a.IpAddressUI,
                pingState = a.PingStateUI,
                pingResponseTime = a.PingResponseTimeUI,
                cpuUsage = a.CpuUsageUI,
                memoryAvailable = a.MemoryAvailableUI,
                memoryTotal = a.MemoryTotalUI,
                memoryUsage = a.MemoryUsageUI
            })).ToList(); 
            return Json(itemList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
