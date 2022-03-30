using AspMVC_Monitor.Controllers.Json;
using AspMVC_Monitor.Models;
using AspMVC_Monitor.Services.SingletonServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AspMVC_Monitor.Controllers
{
    public class DetailsController : Controller
    {
        private readonly ILogger<DetailsController> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IAssetsLiveDataShared _assetsLiveDataShared;

        public DetailsController(ILogger<DetailsController> logger,
            IServiceScopeFactory scopeFactory,
            IAssetsLiveDataShared assetsLiveDataShared)
        {
            this._logger = logger;
            this._scopeFactory = scopeFactory;
            this._assetsLiveDataShared = assetsLiveDataShared;
        }

        public IActionResult Index(int? assetId)
        {
            var viewModel = new DetailsViewModel() { AssetId = assetId };
            var assets = new List<DetailsAsset>();
            assets.AddRange(_assetsLiveDataShared.AssetsData
                .Select(a => new DetailsAsset() { Id = a.Id, Name = a.Name }));
            
            viewModel.Assets = assets;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult GetAssetLiveData(int? assetId)
        {
            if(assetId == null)
            {
                return Json(null);
            }

            var asset = _assetsLiveDataShared.AssetsData.FirstOrDefault(a=>a.Id == assetId);
            if(asset == null)
            {
                return Json(null);
            }
            var assetJ = new assetJson();

            assetJ.id = asset.Id;
            assetJ.name = asset.Name;
            assetJ.ipAddress = asset.IpAddress;
            assetJ.inAlarm = asset.InAlarm;
            assetJ.tags = new List<assetTagJson>();

            assetJ.tags.AddRange(asset.Tags.Values
                .Select(tag => new assetTagJson()
                {
                    id = tag.Id,
                    tagname = tag.Tagname,
                    dataType = tag.DataType.ToString(),
                    value = tag.Value,
                    inAlarm = tag.InAlarm,
                    rangeMax = tag.RangeMax,
                    rangeMin = tag.RangeMin,
                }));

            return Json(assetJ);
        }

        [HttpPost]
        public void UpdateAssetSnmpData(int? assetId)
        {
            if(assetId == null)
            {
                return;
            }
            _assetsLiveDataShared.UpdateAssetSnmpData((int)assetId);
        }

    }
}
