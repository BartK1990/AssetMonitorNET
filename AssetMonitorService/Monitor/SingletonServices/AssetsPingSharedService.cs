using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace AssetMonitorService.Monitor.SingletonServices
{
    public class AssetsPingSharedService : AssetsSharedServiceBase<AssetPing>, IAssetsPingSharedService
    {
        public AssetsPingSharedService(IServiceScopeFactory scopeFactory) : base(scopeFactory)
        {
        }

        protected override void UpdateAssetsList(IAssetMonitorRepository repository)
        {
            AssetsData = new List<AssetPing>();
            var assets = repository.GetAllAssetsAsync().Result.ToList();
            AssetsData.AddRange(assets.Select(a => new AssetPing() { IpAddress = a.IpAddress }));
        }
    }
}
