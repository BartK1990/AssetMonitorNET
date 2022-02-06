using AssetMonitorService.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace AssetMonitorService.Monitor.HostedServices
{
    public class AssetsHistoryTimedService : AssetsTimedServiceBase<AssetsHistoryTimedService>
    {
        protected readonly IServiceScopeFactory _scopeFactory;

        public AssetsHistoryTimedService(IServiceScopeFactory scopeFactory,
            
            ILogger<AssetsHistoryTimedService> logger, TimeSpan? scanTime = null) : base(logger: logger,scanTime: scanTime)
        {
            this._scopeFactory = scopeFactory;
        }

        protected override void TimedJob(object state)
        {
            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IAssetMonitorHistoryDapperRepository>();
           
        }
    }
}
