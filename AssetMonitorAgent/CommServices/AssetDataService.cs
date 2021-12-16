using AssetMonitorAgent.SingletonServices;
using AssetMonitorSharedGRPC.Agent;
using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc;
using System.Threading.Tasks;

namespace AssetMonitorAgent.CommServices
{
    public class AssetDataService : IAssetDataService
    {
        private readonly ILogger<AssetDataService> _logger;
        private readonly IAssetDataSharedService _assetDataSharedService;

        public AssetDataService(ILogger<AssetDataService> logger, IAssetDataSharedService assetDataSharedService)
        {
            this._logger = logger;
            this._assetDataSharedService = assetDataSharedService;
        }

        public Task<AssetDataReply> GetAssetDataAsync(AssetDataRequest request, CallContext context = default)
        {
            _logger.LogInformation($"Replying to {context.ServerCallContext.Peer}");
            return Task.FromResult(new AssetDataReply
            {
                CpuUsage = _assetDataSharedService.CpuUsage,
                MemoryAvailableMB = _assetDataSharedService.MemoryAvailableMB,
                MemoryTotalMB = _assetDataSharedService.MemoryTotalMB
            });
        }
    }
}
