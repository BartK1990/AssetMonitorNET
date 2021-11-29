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
        private readonly IAssetHolder _assetHolder;

        public HomeController(ILogger<HomeController> logger, IAssetHolder assetHolder)
        {
            this._logger = logger;
            this._assetHolder = assetHolder;
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
            return View(_assetHolder.AssetList);
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
            var itemList = _assetHolder.AssetList.Select((a => new { 
                name = a.Name, 
                ipAddress = a.IpAddress,
                pingState = a.PingState,
                pingResponseTime = a.PingResponseTime,
                cpuUsage = a.CpuUsage.ToString("#.##"), // 2 decimal places
                memoryAvailable = a.MemoryAvailable
            })).ToList(); 
            return Json(itemList);
        }

        [HttpPost]
        public IActionResult AddElement()
        {
            _assetHolder.AddAsset(HttpContext.Request.Form["NameText"].ToString(),
                HttpContext.Request.Form["IpAddressText"].ToString());

            return View("Monitor", _assetHolder.AssetList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
