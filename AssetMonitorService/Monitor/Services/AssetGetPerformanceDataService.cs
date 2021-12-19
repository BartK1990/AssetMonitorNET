using AssetMonitorService.gRPC;
using AssetMonitorSharedGRPC.Agent;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services
{
    public class AssetGetPerformanceDataService : IAssetGetPerformanceDataService
    {
        private ILogger<AssetGetPerformanceDataService> _logger;

        public AssetGetPerformanceDataService(ILogger<AssetGetPerformanceDataService> logger)
        {
            this._logger = logger;
        }

        public async Task GetAssetsDataAsync(string hostname, int tcpPort)
        {
            try
            {
                var client = GrpcHelper<IAssetDataService>.CreateClient(hostname, tcpPort);
                var reply = await client.GetAssetDataAsync(
                    new AssetDataRequest { Init = 1 });
                _logger.LogInformation(reply.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Cannot retrieve data from Agent: {hostname}:{tcpPort}. Exception: {ex.Message}");
            }
        }

    }
}
