using AssetMonitorService.Monitor.SingletonServices.Email;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace AssetMonitorService.Monitor.HostedServices
{
    public class AssetsNotificationTimedService : AssetsTimedServiceBase<AssetsNotificationTimedService>
    {
        private readonly IAssetsNotificationDataSharedService _assetsNotificationDataShared;
        protected readonly IServiceScopeFactory _scopeFactory;

        protected readonly object taskTimedJobLock = new object();

        public AssetsNotificationTimedService(
            IAssetsNotificationDataSharedService assetsNotificationDataShared,
            IServiceScopeFactory scopeFactory, ILogger<AssetsNotificationTimedService> logger, TimeSpan? scanTime = null) : base(logger: logger, scanTime: scanTime)
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
    }
}
