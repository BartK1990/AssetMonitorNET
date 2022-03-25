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
            if (tagSetId == null)
            {
                return Json(null);
            }
            var assetListJson = new List<assetJson>();
            foreach (var asset in _assetsLiveDataShared.AssetsData)
            {
                var assetJson = new assetJson();
                assetListJson.Add(assetJson);

                assetJson.name = asset.Name;
                assetJson.ipAddress = asset.IpAddress;
                assetJson.inAlarm = asset.InAlarm;
                assetJson.tags = new List<assetTagJson>();

                int tagSetIdNotNull = (int)tagSetId;
                if (asset.TagsIdForSharedTagSets.ContainsKey(tagSetIdNotNull))
                {
                    foreach (var tagId in asset.TagsIdForSharedTagSets[tagSetIdNotNull])
                    {
                        assetJson.tags.Add(new assetTagJson(asset.Tags[tagId.Value], tagId.Key));
                    }
                }
            }
            return Json(assetListJson);
        }

        private class assetJson
        {
            public string name { get; set; }
            public string ipAddress { get; set; }
            public bool inAlarm { get; set; }

            public List<assetTagJson> tags { get; set; }
        }

        private class assetTagJson
        {
            public assetTagJson(TagLiveValue tag, int sharedId)
            {
                this.sharedTagId = sharedId;
                this.tagname = tag.Tagname;
                this.dataType = tag.DataType.ToString();
                this.value = tag.Value;
                this.inAlarm = tag.InAlarm;
                this.rangeMax = tag.RangeMax;
                this.rangeMin = tag.RangeMin;
            }

            public int sharedTagId { get; set; }
            public string tagname { get; set; }
            public string dataType { get; set; }
            public object value { get; set; }
            public bool inAlarm { get; set; }
            public double? rangeMax { get; set; }
            public double? rangeMin { get; set; }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
