using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace AssetMonitorService.Monitor.SingletonServices
{
    public class AssetsPerformanceDataSharedService : AssetsSharedServiceBase<AssetPerformanceData>, IAssetsPerformanceDataSharedService
    {
        public AssetsPerformanceDataSharedService(IServiceScopeFactory scopeFactory) : base(scopeFactory)
        {
        }

        protected override void UpdateAssetsList(IAssetMonitorRepository repository)
        {
            AssetsData = new List<AssetPerformanceData>();
            var assets = repository.GetWindowsAssetsAsync().Result.ToList();
            AssetsData.AddRange(assets.Select(a => new AssetPerformanceData()));
        }
    }
}
