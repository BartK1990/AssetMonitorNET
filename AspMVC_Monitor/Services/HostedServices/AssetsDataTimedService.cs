using AspMVC_Monitor.Services.ScopedServices;
using AspMVC_Monitor.Services.SingletonServices;
using AssetMonitorDataAccess.Models.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Services.HostedServices
{
    public class AssetsDataTimedService : TimedServiceBase<AssetsDataTimedService>
    {
        private const int ScanTimeInSecondsDefault = 10;

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IAssetsLiveDataShared _assetsLiveDataShared;

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
            _assetsLiveDataShared.UpdateAssetsLiveData().Wait();
        }
    }
}
