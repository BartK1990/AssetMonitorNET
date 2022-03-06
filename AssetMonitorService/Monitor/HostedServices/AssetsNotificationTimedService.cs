using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorService.Monitor.Services;
using AssetMonitorService.Monitor.SingletonServices.Email;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AssetMonitorService.Monitor.HostedServices
{
    public class AssetsNotificationTimedService : AssetsTimedServiceBase<AssetsNotificationTimedService>
    {
        private const int ScanTimeInSecondsDefault = 10;

        private readonly IAssetsNotificationDataSharedService _assetsNotificationDataShared;
        protected readonly IServiceScopeFactory _scopeFactory;

        protected readonly object taskTimedJobLock = new object();

        public AssetsNotificationTimedService(
            IAssetsNotificationDataSharedService assetsNotificationDataShared,
            IServiceScopeFactory scopeFactory, 
            ILogger<AssetsNotificationTimedService> logger,
            IApplicationPropertiesService appProperties
            ) : base(logger: logger, appProperties: appProperties)
        {
            this._assetsNotificationDataShared = assetsNotificationDataShared;
            this._scopeFactory = scopeFactory;
        }

        protected override void TimedJob()
        {
            lock (taskTimedJobLock)
            {
                _assetsNotificationDataShared.SendEmailNotifications();
            }
        }

        protected override int GetScanTimeInSeconds()
        {
            return _appProperties.GetProperty(ApplicationPropertyNameEnum.AssetsNotificationTimedScanTime, int.Parse, ScanTimeInSecondsDefault).Result;
        }
    }
}
