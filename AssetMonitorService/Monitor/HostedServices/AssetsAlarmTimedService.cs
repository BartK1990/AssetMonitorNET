using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorService.Monitor.Services;
using AssetMonitorService.Monitor.SingletonServices.Alarm;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AssetMonitorService.Monitor.HostedServices
{
    public class AssetsAlarmTimedService : AssetsTimedServiceBase<AssetsAlarmTimedService>
    {
        private const int ScanTimeInSecondsDefault = 10;

        protected readonly IServiceScopeFactory _scopeFactory;
        private readonly IAssetsAlarmDataSharedService _assetsAlarmDataShared;

        protected readonly object taskTimedJobLock = new object();

        public AssetsAlarmTimedService(IServiceScopeFactory scopeFactory,
            IAssetsAlarmDataSharedService assetsAlarmDataShared,
            ILogger<AssetsAlarmTimedService> logger,
            IApplicationPropertiesService appProperties
            ) : base(logger: logger, appProperties: appProperties)
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

        protected override int GetScanTimeInSeconds()
        {
            return _appProperties.GetProperty(ApplicationPropertyNameEnum.AssetsAlarmTimedScanTime, int.Parse, ScanTimeInSecondsDefault).Result;
        }
    }
}
