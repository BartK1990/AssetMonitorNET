using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.SingletonServices
{
    public class AssetsPingSharedService : AssetsSharedServiceBase<AssetPing>, IAssetsPingSharedService
    {
        public AssetsPingSharedService(IServiceScopeFactory scopeFactory) : base(scopeFactory)
        {
            UpdateAssetsListBase().Wait();
        }

        protected override async Task UpdateAssetsList(IAssetMonitorRepository repository)
        {
            var assets = (await repository.GetAllAssetsAsync()).ToList();
            AssetsData.AddRange(assets.Select(a => new AssetPing() { Id = a.Id, IpAddress = a.IpAddress }));
        }
    }
}
