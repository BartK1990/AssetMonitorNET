using AspMVC_Monitor.Services.ScopedServices;
using AspMVC_Monitor.Services.SingletonServices;
using AssetMonitorDataAccess.Models.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspMVC_Monitor.Services.HostedServices
{
    public class AssetsDataTimedService : TimedServiceBase<AssetsDataTimedService>
    {
        private const int ScanTimeInSecondsDefault = 10;

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IAssetsLiveDataShared _assetsLiveDataShared;

        protected readonly object taskTimedJobLock = new object();

        public AssetsDataTimedService(IServiceScopeFactory scopeFactory,
            ILogger<AssetsDataTimedService> logger,
            IApplicationPropertiesService appProperties,
            IAssetsLiveDataShared assetsLiveDataShared
            ) : base(logger: logger, appProperties: appProperties)
        {
            this._scopeFactory = scopeFactory;
            this._assetsLiveDataShared = assetsLiveDataShared;
        }

        protected override int GetScanTimeInSeconds()
        {
            return _appProperties.GetProperty(ApplicationPropertyNameEnum.FrontEndScanTime, int.Parse, ScanTimeInSecondsDefault).Result;
        }

        protected override void TimedJob()
        {
            lock (taskTimedJobLock)
            {
                _assetsLiveDataShared.UpdateAssetsLiveData().Wait();
            }
        }
    }
}
