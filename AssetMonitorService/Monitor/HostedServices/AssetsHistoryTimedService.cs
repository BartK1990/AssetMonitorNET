using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.SingletonServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace AssetMonitorService.Monitor.HostedServices
{
    public class AssetsHistoryTimedService : AssetsTimedServiceBase<AssetsHistoryTimedService>
    {
        protected readonly IServiceScopeFactory _scopeFactory;
        private readonly IAssetsPingSharedService _assetsPingShared;
        private readonly IAssetsPerformanceDataSharedService _assetsPerformanceDataShared;
        private readonly IAssetsSnmpDataSharedService _assetsSnmpDataShared;

        public AssetsHistoryTimedService(IServiceScopeFactory scopeFactory,
            IAssetsPingSharedService assetsPingShared,
            IAssetsPerformanceDataSharedService assetsPerformanceDataShared,
            IAssetsSnmpDataSharedService assetsSnmpDataShared,
            ILogger<AssetsHistoryTimedService> logger, TimeSpan? scanTime = null) : base(logger: logger,scanTime: scanTime)
        {
            this._scopeFactory = scopeFactory;
            this._assetsPingShared = assetsPingShared;
            this._assetsPerformanceDataShared = assetsPerformanceDataShared;
            this._assetsSnmpDataShared = assetsSnmpDataShared;
        }

        protected override void TimedJob(object state)
        {
            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IAssetMonitorHistoryDapperRepository>();
           
        }
    }
}
