using AssetMonitorService.Monitor.SingletonServices.Alarm;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace AssetMonitorService.Monitor.HostedServices
{
    public class AssetsAlarmTimedService : AssetsTimedServiceBase<AssetsAlarmTimedService>
    {
        protected readonly IServiceScopeFactory _scopeFactory;
        private readonly IAssetsAlarmDataSharedService _assetsAlarmDataShared;

        protected readonly object taskTimedJobLock = new object();

        private DateTime _lastSaveTimeToDatebase;

        public AssetsAlarmTimedService(IServiceScopeFactory scopeFactory,
            IAssetsAlarmDataSharedService assetsAlarmDataShared,
            ILogger<AssetsAlarmTimedService> logger, TimeSpan? scanTime = null) : base(logger: logger, scanTime: scanTime)
        {
            this._scopeFactory = scopeFactory;
            this._assetsAlarmDataShared = assetsAlarmDataShared;
        }

        protected override void TimedJob()
        {
            // Start all tasks for getting and saving the data
            lock (taskTimedJobLock)
            {
                GetAssetsData();
            }
        }

        private void GetAssetsData()
        {
            _assetsAlarmDataShared.UpdateAssetsAlarmValues();
        }
    }
}
