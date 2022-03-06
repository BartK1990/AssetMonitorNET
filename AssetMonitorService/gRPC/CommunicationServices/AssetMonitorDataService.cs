using AssetMonitorService.Monitor.Services.Asset.Live;
using AssetMonitorService.Monitor.SingletonServices;
using AssetMonitorService.Monitor.SingletonServices.Historical;
using AssetMonitorSharedGRPC.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.gRPC.CommunicationServices
{
    public class AssetMonitorDataService : IAssetMonitorDataService
    {
        private readonly ILogger<AssetMonitorDataService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IAssetsIcmpSharedService _assetsPingDataShared;
        private readonly IAssetsPerformanceDataSharedService _assetsPerformanceDataShared;
        private readonly IAssetsSnmpDataSharedService _assetsSnmpDataShared;
        private readonly IAssetSnmpDataService _assetSnmpDataService;
        private readonly IAssetsHistoricalDataSharedService _assetsHistoricalDataShared;

        public AssetMonitorDataService(ILogger<AssetMonitorDataService> logger,
            IServiceScopeFactory scopeFactory,
            IAssetsIcmpSharedService assetsPingDataShared,
            IAssetsPerformanceDataSharedService assetsPerformanceDataShared,
            IAssetsSnmpDataSharedService assetsSnmpDataShared,
            IAssetSnmpDataService assetSnmpDataService,
            IAssetsHistoricalDataSharedService assetsHistoricalDataShared)
        {
            this._logger = logger;
            this._scopeFactory = scopeFactory;
            this._assetsPingDataShared = assetsPingDataShared;
            this._assetsPerformanceDataShared = assetsPerformanceDataShared;
            this._assetsSnmpDataShared = assetsSnmpDataShared;
            this._assetSnmpDataService = assetSnmpDataService;
            this._assetsHistoricalDataShared = assetsHistoricalDataShared;
        }

        public async Task<AssetSnmpUpdateCommandReply> UpdateAssetSnmpValuesById(AssetSnmpUpdateCommandRequest request, CallContext context = default)
        {
            _logger.LogInformation($"Asset (Id: {request.AssetId}) SNMP update command from {context.ServerCallContext.Peer}");
            var reply = new AssetSnmpUpdateCommandReply();

            var assetSnmpDataShared = _assetsSnmpDataShared.AssetsData.FirstOrDefault(a => a.Id == request.AssetId);
            if(assetSnmpDataShared == null)
            {
                reply.Success = false;
                return await Task.FromResult(reply);
            }

            await _assetSnmpDataService.UpdateAssetOnDemandData(_assetsSnmpDataShared.AssetsData.FirstOrDefault(a=>a.Id == request.AssetId));
            reply.Success = await _assetsHistoricalDataShared.UpdateAssetActualSnmpValuesByIdAsync(request.AssetId);

            return await Task.FromResult(reply);
        }

    }
}
