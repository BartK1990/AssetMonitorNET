using AssetMonitorDataAccess.Models;
using AssetMonitorService.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.SingletonServices
{
    public class AssetsCollectionSharedService : AssetsSharedServiceBase<AssetsCollectionSharedService, Asset>, 
        IAssetsCollectionSharedService
    {
        public AssetsCollectionSharedService(IServiceScopeFactory scopeFactory,
            ILogger<AssetsCollectionSharedService> logger) : base(scopeFactory: scopeFactory, logger: logger)
        {
        }

        protected override async Task UpdateAssetsList(IAssetMonitorRepository repository)
        {
            var assets = (await repository.GetAllAssetsAsync()).ToList();
            AssetsData.AddRange(assets);
        }
    }
}
