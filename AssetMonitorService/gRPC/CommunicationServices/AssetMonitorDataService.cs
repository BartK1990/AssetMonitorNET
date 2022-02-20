using AssetMonitorDataAccess.Models;
using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.SingletonServices;
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

        public AssetMonitorDataService(ILogger<AssetMonitorDataService> logger,
            IServiceScopeFactory scopeFactory,
            IAssetsIcmpSharedService assetsPingDataShared,
            IAssetsPerformanceDataSharedService assetsPerformanceDataShared,
            IAssetsSnmpDataSharedService assetsSnmpDataShared)
        {
            this._logger = logger;
            this._scopeFactory = scopeFactory;
            this._assetsPingDataShared = assetsPingDataShared;
            this._assetsPerformanceDataShared = assetsPerformanceDataShared;
            this._assetsSnmpDataShared = assetsSnmpDataShared;
        }

        public async Task<AssetsPerformanceDataReply> GetAssetsPerformanceData(AssetsPerformanceDataRequest request, CallContext context = default)
        {
            _logger.LogInformation($"Replying performance data to {context.ServerCallContext.Peer}");
            var reply = new AssetsPerformanceDataReply
            {
                AssetsData = _assetsPerformanceDataShared.AssetsData
                .Select(a => new AssetsPerformanceDataMessage()
                {
                    Id = a.Id,
                    IpAddress = a.IpAddress,
                })
            };

            return await Task.FromResult(reply);
        }

        public async Task<AssetsPingDataReply> GetAssetsPingData(AssetsPingDataRequest request, CallContext context = default)
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

            return await Task.FromResult(reply);
        }

        public async Task<AssetSnmpUpdateCommandReply> UpdateAssetSnmpValuesById(AssetSnmpUpdateCommandRequest request, CallContext context = default)
        {
            _logger.LogInformation($"Asset (Id: {request.AssetId}) SNMP update command from {context.ServerCallContext.Peer}");
            var reply = new AssetSnmpUpdateCommandReply();

            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IAssetMonitorRepository>();

            var snmpAssetValues = (await repository.GetSnmpAssetValuesByAssetIdAsync(request.AssetId)).ToList();
            var snmpTags = (await repository.GetSnmpAssetTagsByAssetIdAsync(request.AssetId)).ToList();

            var assetData = _assetsSnmpDataShared.AssetsData.FirstOrDefault(ad=>ad.Id == request.AssetId);

            foreach (var tag in snmpTags)
            {
                if (!assetData.Data.ContainsKey(tag))
                {
                    continue;
                }
                var snmpTagValue = assetData.Data[tag].Value;
                var valueToUpdate = snmpAssetValues.FirstOrDefault(sa => sa.SnmpTagId == tag.Id);
                if (valueToUpdate == null)
                {
                    repository.Add(new SnmpAssetValue() { AssetId = request.AssetId, SnmpTag = tag, Value = snmpTagValue.ToString() });
                    continue;
                }
                valueToUpdate.Value = snmpTagValue.ToString();
                repository.Update(valueToUpdate);
            }

            reply.Success = await repository.SaveAllAsync();
            return await Task.FromResult(reply);
        }
    }
}
