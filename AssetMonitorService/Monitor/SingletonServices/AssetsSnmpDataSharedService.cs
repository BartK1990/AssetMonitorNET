using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.SingletonServices
{
    public class AssetsSnmpDataSharedService : AssetsSharedServiceBase<AssetSnmpData>, IAssetsSnmpDataSharedService
    {
        private readonly ILogger<AssetsSnmpDataSharedService> _logger;

        public AssetsSnmpDataSharedService(IServiceScopeFactory scopeFactory,
            ILogger<AssetsSnmpDataSharedService> logger) : base(scopeFactory)
        {
            this._logger = logger;
        }

        protected override void UpdateAssetsList(IAssetMonitorRepository repository)
        {
            throw new NotImplementedException();
        }
    }
}
