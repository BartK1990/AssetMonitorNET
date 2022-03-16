using AspMVC_Monitor.Services.ScopedServices;
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

        public AssetsDataTimedService(IServiceScopeFactory scopeFactory,
            ILogger<AssetsDataTimedService> logger,
            IApplicationPropertiesService appProperties
            ) : base(logger: logger, appProperties: appProperties)
        {
            this._scopeFactory = scopeFactory;
        }

        protected override int GetScanTimeInSeconds()
        {
            return _appProperties.GetProperty(ApplicationPropertyNameEnum.FrontEndScanTime, int.Parse, ScanTimeInSecondsDefault).Result;
        }

        protected override void TimedJob()
        {
            throw new NotImplementedException();
        }
    }
}
