using AspMVC_Monitor.Data.Repositories;
using AspMVC_Monitor.gRPC;
using AssetMonitorSharedGRPC.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Models
{
    public class AssetsMonitor : IAssetsMonitor
    {
        private readonly ILogger<AssetsMonitor> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public const int TcpPort = 9561; // ToDo make configurable in DB

        public List<AssetLiveData> AssetsList { get; set; }

        public AssetsMonitor(ILogger<AssetsMonitor> logger,
            IServiceScopeFactory scopeFactory)
        {
            this._logger = logger;
            this._scopeFactory = scopeFactory;

            AssetsList = new List<AssetLiveData>();

            UpdateAssetsList();
        }

        public void UpdateAssetsList()
        {
            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IAssetMonitorRepository>();
            var assets = repository.GetAllAssetsAsync().Result.ToList();
            foreach (var a in assets)
            {
                if (AssetsList.Select(a => a.Id).Contains(a.Id))
                {
                    continue;
                }
                AssetsList.Add(new AssetLiveData()
                {
                    Id = a.Id,
                    Name = a.Name,
                    IpAddress = a.IpAddress
                });
            }
        }
        public async Task UpdateAssetsListAsync()
        {
            await Task.Run(() => UpdateAssetsList());
        }
    }
}
