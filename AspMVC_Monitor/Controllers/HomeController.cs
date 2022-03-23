using AspMVC_Monitor.Data.Repositories;
using AspMVC_Monitor.Models;
using AspMVC_Monitor.Services.SingletonServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IAssetsLiveDataShared _assetsLiveDataShared;

        public HomeController(ILogger<HomeController> logger,
            IServiceScopeFactory scopeFactory,
            IAssetsLiveDataShared assetsLiveDataShared)
        {
            this._logger = logger;
            this._scopeFactory = scopeFactory;
            this._assetsLiveDataShared = assetsLiveDataShared;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Monitor(int? tagSetId)
        {
            HttpContext.Session.SetString("Time", DateTime.Now.ToString());

            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IAssetMonitorRepository>();

            var tagSharedSets = (await repository.GetAllTagSharedSetsAsync()).ToList();

            var monitorTagShared = new MonitorViewModel() { TagSets = tagSharedSets
                .Select(s => new MonitorTagSharedSet() { Id = s.Id, Name = s.Name }).ToList() };

            if (tagSetId != null)
            {
                _logger.LogError(tagSetId.ToString());
            }

            return View(monitorTagShared);
        }

        [HttpPost]
        public IActionResult GetServerTime()
        {
            HttpContext.Session.SetString("Time", DateTime.Now.ToString());
            return Json(new { data = HttpContext.Session.GetString("Time") });
        }

        //[HttpPost]
        //public IActionResult GetAssetList()
        //{
        //    var itemList = _assetsMonitor.AssetsList.Select((a => new { 
        //        name = a.NameUI, 
        //        ipAddress = a.IpAddressUI,
        //        pingState = a.PingStateUI,
        //        pingResponseTime = a.PingResponseTimeUI,
        //        cpuUsage = a.CpuUsageUI,
        //        memoryAvailable = a.MemoryAvailableUI,
        //        memoryTotal = a.MemoryTotalUI,
        //        memoryUsage = a.MemoryUsageUI
        //    })).ToList(); 
        //    return Json(itemList);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
