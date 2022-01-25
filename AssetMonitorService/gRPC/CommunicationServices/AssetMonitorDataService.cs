using AssetMonitorService.Monitor.SingletonServices;
using AssetMonitorSharedGRPC.Server;
using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.gRPC.CommunicationServices
{
    public class AssetMonitorDataService : IAssetMonitorDataService
    {
        private readonly ILogger<AssetMonitorDataService> _logger;
        private readonly IAssetsPingSharedService _assetsPingDataShared;
        private readonly IAssetsPerformanceDataSharedService _assetsPerformanceDataShared;

        public AssetMonitorDataService(ILogger<AssetMonitorDataService> logger,
            IAssetsPingSharedService assetsPingDataShared,
            IAssetsPerformanceDataSharedService assetsPerformanceDataShared)
        {
            this._logger = logger;
            this._assetsPingDataShared = assetsPingDataShared;
            this._assetsPerformanceDataShared = assetsPerformanceDataShared;
        }

        public Task<AssetsPerformanceDataReply> GetAssetsPerformanceData(AssetsPerformanceDataRequest request, CallContext context = default)
        {
            _logger.LogInformation($"Replying performance data to {context.ServerCallContext.Peer}");
            var reply = new AssetsPerformanceDataReply
            {
                AssetsData = _assetsPerformanceDataShared.AssetsData
                .Select(a => new AssetsPerformanceDataMessage()
                {
                    Id = a.Id,
                    IpAddress = a.IpAddress,
                    //TcpPort = a.TcpPort,
                    //CpuUsage = a.CpuUsage,
                    //MemoryAvailableMB = a.MemoryAvailable,
                    //MemoryTotalMB = a.MemoryTotal
                })
            };

            return Task.FromResult(reply);
        }

        public Task<AssetsPingDataReply> GetAssetsPingData(AssetsPingDataRequest request, CallContext context = default)
        {
            _logger.LogInformation($"Replying ping data to {context.ServerCallContext.Peer}");
            var reply = new AssetsPingDataReply
            {
                AssetsData = _assetsPingDataShared.AssetsData
                .Select(a => new AssetsPingDataMessage()
                {
                    Id = a.Id,
                    IpAddress = a.IpAddress,
                    PingState = a.PingState,
                    PingResponseTime = a.PingResponseTime
                })
            };

            return Task.FromResult(reply);
        }
    }
}
