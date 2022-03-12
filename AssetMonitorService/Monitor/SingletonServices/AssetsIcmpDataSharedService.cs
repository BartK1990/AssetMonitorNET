using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Model.Live;
using AssetMonitorService.Monitor.Model.TagConfig;
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
            var assets = (await repository.GetIcmpAssetsAsync()).ToList();
            foreach (var asset in assets)
            {
                var tags = (await repository.GetIcmpTagsByAssetIdAsync(asset.Id)).ToList();

                var icmpTags = tags
                    .Select(tag => new TagIcmp(tag)).ToList();

                AssetsData.Add(new AssetIcmpData(icmpTags)
                {
                    Id = asset.Id,
                    IpAddress = asset.IpAddress
                });
            }
        }
    }
}
