using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.SingletonServices
{
    public class AssetsPingDataSharedService : AssetsSharedServiceBase<AssetsPingDataSharedService, AssetPing>, 
        IAssetsPingSharedService
    {
        public AssetsPingDataSharedService(IServiceScopeFactory scopeFactory,
            ILogger<AssetsPingDataSharedService> logger) : base(scopeFactory: scopeFactory, logger: logger)
        {
        }

        protected override async Task UpdateAssetsList(IAssetMonitorRepository repository)
        {
            var assets = (await repository.GetAllAssetsAsync()).ToList();
            AssetsData.AddRange(assets.Select(a => new AssetPing() { Id = a.Id, IpAddress = a.IpAddress }));
        }
    }
}
