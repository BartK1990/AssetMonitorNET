using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Services;
using AssetMonitorService.Monitor.SingletonServices;
using AssetMonitorService.Monitor.SingletonServices.Historical;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace AssetMonitorService.Monitor.HostedServices
{
    public class AssetsHistoryTimedService : AssetsTimedServiceBase<AssetsHistoryTimedService>
    {
        private const int ScanTimeInSecondsDefault = 10;

        protected readonly IServiceScopeFactory _scopeFactory;
        private readonly IAssetsIcmpSharedService _assetsPingShared;
        private readonly IAssetsPerformanceDataSharedService _assetsPerformanceDataShared;
        private readonly IAssetsSnmpDataSharedService _assetsSnmpDataShared;
        private readonly IAssetsHistoricalDataSharedService _assetsHistoricalDataShared;
        private readonly IHistoricalTablesSharedService _historicalTablesShared;

        protected readonly object taskTimedJobLock = new object();

        private DateTime _lastSaveTimeToDatebase;

        public AssetsHistoryTimedService(IServiceScopeFactory scopeFactory,
            IAssetsIcmpSharedService assetsPingShared,
            IAssetsPerformanceDataSharedService assetsPerformanceDataShared,
            IAssetsSnmpDataSharedService assetsSnmpDataShared,
            IAssetsHistoricalDataSharedService assetsHistoricalDataShared,
            IHistoricalTablesSharedService historicalTablesShared,
            ILogger<AssetsHistoryTimedService> logger,
            IApplicationPropertiesService appProperties
            ) : base(logger: logger, appProperties: appProperties)
        {
            this._scopeFactory = scopeFactory;
            this._assetsPingShared = assetsPingShared;
            this._assetsPerformanceDataShared = assetsPerformanceDataShared;
            this._assetsSnmpDataShared = assetsSnmpDataShared;
            this._assetsHistoricalDataShared = assetsHistoricalDataShared;
            this._historicalTablesShared = historicalTablesShared;

            _lastSaveTimeToDatebase = DateTime.UtcNow; // Added for not creating new historical data at application start
        }

        protected override void TimedJob()
        {
            // Start all tasks for getting and saving the data
            lock (taskTimedJobLock)
            {
                GetAssetsData();
                SaveAssetsTenMinData();
            }
        }

        private void GetAssetsData()
        {
            _assetsHistoricalDataShared.UpdateAssetsHistoricalValues();
        }

        private void SaveAssetsTenMinData()
        {
            var utcNow = DateTime.UtcNow;

            // Skip execution if it was already executed in this 10 min period
            if ((_lastSaveTimeToDatebase.Date == utcNow.Date)
                && (_lastSaveTimeToDatebase.Hour == utcNow.Hour)
                && ((_lastSaveTimeToDatebase.Minute / 10) == (utcNow.Minute / 10)))
            {
                return;
            }
            var timeFormat = "yyyy-MM-dd HH:mm";
            var tenMinTimeStamp = utcNow.ToString(timeFormat).Substring(0, timeFormat.Length - 1) + "0:00";

            // Below few lines only for testing - saves data every minute
            //if (_lastSaveTimeToDatebase.ToString("yyyy-MM-dd HH:mm") == DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm"))
            //    {
            //        return;
            //    }
            //var timeFormat = "yyyy-MM-dd HH:mm";
            //var tenMinTimeStamp = DateTime.UtcNow.ToString(timeFormat) + ":00";

            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IAssetMonitorHistoryDapperRepository>();

            _historicalTablesShared.InsertTimedDataForAllAssetsAsync(tenMinTimeStamp).Wait();
            _lastSaveTimeToDatebase = utcNow;

            // ToDo remove this when User interface is created
            var repositoryAssets = scope.ServiceProvider.GetRequiredService<IAssetMonitorRepository>();
            var assets = repositoryAssets.GetSnmpAssetsAsync().Result.ToList();
            foreach (var asset in assets)
            {
                _assetsHistoricalDataShared.UpdateAssetActualSnmpValuesByIdAsync(asset.Id);
            }
        }

        protected override int GetScanTimeInSeconds()
        {
            return _appProperties.GetProperty(ApplicationPropertyNameEnum.AssetsHistoryTimedScanTime, int.Parse, ScanTimeInSecondsDefault).Result;
        }

    }
}
