using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.SingletonServices;
using AssetMonitorService.Monitor.SingletonServices.Historical;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.HostedServices
{
    public class AssetsHistoryTimedService : AssetsTimedServiceBase<AssetsHistoryTimedService>
    {
        protected readonly IServiceScopeFactory _scopeFactory;
        private readonly IAssetsPingSharedService _assetsPingShared;
        private readonly IAssetsPerformanceDataSharedService _assetsPerformanceDataShared;
        private readonly IAssetsSnmpDataSharedService _assetsSnmpDataShared;
        private readonly IAssetsHistoricalDataSharedService _assetsHistoricalDataShared;

        protected readonly object taskGetAssetsDataLock = new object();
        protected readonly object taskSaveAssetsTenMinDataLock = new object();

        private DateTime _lastSaveTimeToDatebase;

        public AssetsHistoryTimedService(IServiceScopeFactory scopeFactory,
            IAssetsPingSharedService assetsPingShared,
            IAssetsPerformanceDataSharedService assetsPerformanceDataShared,
            IAssetsSnmpDataSharedService assetsSnmpDataShared,
            IAssetsHistoricalDataSharedService assetsHistoricalDataShared,
            ILogger<AssetsHistoryTimedService> logger, TimeSpan? scanTime = null) : base(logger: logger, scanTime: scanTime)
        {
            this._scopeFactory = scopeFactory;
            this._assetsPingShared = assetsPingShared;
            this._assetsPerformanceDataShared = assetsPerformanceDataShared;
            this._assetsSnmpDataShared = assetsSnmpDataShared;
            this._assetsHistoricalDataShared = assetsHistoricalDataShared;
        }

        protected override void TimedJob(object state)
        {
            // Start all tasks for getting and saving the data
            lock (taskGetAssetsDataLock)
            {
                GetAssetsData();
            }
            lock (taskSaveAssetsTenMinDataLock)
            {
                _ = SaveAssetsTenMinData();
            }
        }

        private void GetAssetsData()
        {
            _assetsHistoricalDataShared.UpdateAssetsHistoricalValues();
        }

        private async Task SaveAssetsTenMinData()
        {
            // Skip execution if it was already executed in this 10 min period
            if ((_lastSaveTimeToDatebase.Date == DateTime.UtcNow.Date)
                && (_lastSaveTimeToDatebase.Hour == DateTime.UtcNow.Hour)
                && (_lastSaveTimeToDatebase.Minute.ToString().Substring(0, 1) == DateTime.UtcNow.Minute.ToString().Substring(0, 1)))
            {
                return;
            }

            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IAssetMonitorHistoryDapperRepository>();



            _lastSaveTimeToDatebase = DateTime.UtcNow;
        }
    }
}
