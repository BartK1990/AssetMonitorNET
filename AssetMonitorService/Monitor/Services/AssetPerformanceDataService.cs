using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorService.gRPC;
using AssetMonitorService.Monitor.Model;
using AssetMonitorSharedGRPC.Agent;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using WindowsDataLib;

namespace AssetMonitorService.Monitor.Services
{
    public class AssetPerformanceDataService : IAssetPerformanceDataService
    {
        private readonly ILogger<AssetPerformanceDataService> _logger;

        public AssetPerformanceDataService(ILogger<AssetPerformanceDataService> logger)
        {
            this._logger = logger;
        }

        public async Task UpdateAsset(AssetPerformanceData assetPerformanceData)
        {
            if(assetPerformanceData.IpAddress == IPAddress.Loopback.ToString())
            {
                foreach (var d in assetPerformanceData.Data)
                {
                    switch (d.Key.AgentDataTypeId)
                    {
                        case (int)AgentDataTypeEnum.PerformanceCounter:
                            d.Value.Value = AssetPerformance.GetPerformanceCounterValue(d.Key.PerformanceCounter);
                            break;
                        case (int)AgentDataTypeEnum.WMI:
                            d.Value.Value = AssetPerformance.GetWmiValue(d.Key.WmiManagementObject);
                            break;
                        default:
                            break;
                    }
                }
                return;
            }
            //var reply = await GetAssetsDataAsync(assetPerformanceData.IpAddress, assetPerformanceData.TcpPort);

            //assetPerformanceData.CpuUsage = reply.CpuUsage;
            //assetPerformanceData.MemoryAvailable = reply.MemoryAvailableMB;
            //assetPerformanceData.MemoryTotal = reply.MemoryTotalMB;
        }

        private async Task<AssetDataReply> GetAssetsDataAsync(string hostname, int tcpPort)
        {
            AssetDataReply reply = new AssetDataReply();
            try
            {
                var client = GrpcHelper<IAssetDataService>.CreateUnsecureClient(hostname, tcpPort);
                reply = await client.GetAssetDataAsync(
                    new AssetDataRequest { Init = 1 });
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Cannot retrieve data from Agent: {hostname}:{tcpPort}");
                _logger.LogDebug($"Exception: { ex.Message}");
            }
            return reply;
        }

    }
}
