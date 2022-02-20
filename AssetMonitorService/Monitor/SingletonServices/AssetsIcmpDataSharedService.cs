using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.SingletonServices
{
    public class AssetsIcmpDataSharedService : AssetsSharedServiceBase<AssetsIcmpDataSharedService, AssetIcmpData>, 
        IAssetsIcmpSharedService
    {
        public AssetsIcmpDataSharedService(IServiceScopeFactory scopeFactory,
            ILogger<AssetsIcmpDataSharedService> logger) : base(scopeFactory: scopeFactory, logger: logger)
        {
        }

        protected override async Task UpdateAssetsList(IAssetMonitorRepository repository)
        {
            var assets = (await repository.GetAllAssetsAsync()).ToList();
            AssetsData.AddRange(assets.Select(a => new AssetIcmpData() { Id = a.Id, IpAddress = a.IpAddress }));
        }
    }
}
