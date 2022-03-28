using AspMVC_Monitor.Controllers.Json;
using AspMVC_Monitor.Data.Repositories;
using AspMVC_Monitor.Models;
using AspMVC_Monitor.Services.SingletonServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Controllers
{
    public partial class HomeController : Controller
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

        public async Task<IActionResult> Monitor()
        {
            HttpContext.Session.SetString("Time", DateTime.Now.ToString());

            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IAssetMonitorRepository>();

            var tagSharedSets = (await repository.GetAllTagSharedSetsAsync()).ToList();

            var monitorTagShared = new MonitorViewModel()
            {
                TagSets = tagSharedSets
                .Select(s => new MonitorTagSharedSet() { Id = s.Id, Name = s.Name }).ToList()
            };

            return View(monitorTagShared);
        }

        [HttpPost]
        public IActionResult GetServerTime()
        {
            HttpContext.Session.SetString("Time", DateTime.Now.ToString());
            return Json(new { data = HttpContext.Session.GetString("Time") });
        }

        [HttpPost]
        public async Task<IActionResult> GetSharedTagColumns(int? tagSetId)
        { 
            if(tagSetId == null)
            {
                return Json(null);
            }
            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IAssetMonitorRepository>();

            var sharedTags = await repository.GetTagSharedBySetIdAsync((int)tagSetId);
            var tagSetsJson = sharedTags.Select(t => new 
            { 
                id = t.Id, 
                columnName = t.ColumnName 
            });

            return Json(tagSetsJson);
        }

        [HttpPost]
        public IActionResult GetAssetsLiveData(int? tagSetId)
        {
            var assetsJ = new assetsJson();
            assetsJ.assets = new List<assetJson>();
            foreach (var asset in _assetsLiveDataShared.AssetsData)
            {
                var assetJ = new assetJson();
                assetsJ.assets.Add(assetJ);

                assetJ.id = asset.Id;
                assetJ.name = asset.Name;
                assetJ.ipAddress = asset.IpAddress;
                assetJ.inAlarm = asset.InAlarm;
                _ = assetJ.inAlarm ? assetsJ.inAlarmCnt++ : assetsJ.okCnt++;
                assetJ.tags = new List<assetTagJson>();

                if (tagSetId == null)
                {
                    continue;
                }
                int tagSetIdNotNull = (int)tagSetId;
                if (asset.TagsIdForSharedTagSets.ContainsKey(tagSetIdNotNull))
                {
                    foreach (var tagId in asset.TagsIdForSharedTagSets[tagSetIdNotNull])
                    {
                        assetJ.tags.Add(new assetTagJson(asset.Tags[tagId.Value], tagId.Key));
                    }
                }
            }
            return Json(assetsJ);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
