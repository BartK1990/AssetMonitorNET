using AssetMonitorService.Monitor.Services.Asset.Live;
using AssetMonitorService.Monitor.SingletonServices;
using AssetMonitorService.Monitor.SingletonServices.Historical;
using AssetMonitorSharedGRPC.Helpers;
using AssetMonitorSharedGRPC.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.gRPC.CommunicationServices
{
    public class AssetMonitorDataService : IAssetMonitorDataService
    {
        private readonly ILogger<AssetMonitorDataService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IAssetsLiveDataSharedService _assetsLiveDataShared;
        private readonly IAssetsSnmpDataSharedService _assetsSnmpDataShared;
        private readonly IAssetSnmpDataService _assetSnmpDataService;
        private readonly IAssetsHistoricalDataSharedService _assetsHistoricalDataShared;

        public AssetMonitorDataService(ILogger<AssetMonitorDataService> logger,
            IServiceScopeFactory scopeFactory,
            IAssetsLiveDataSharedService assetsLiveDataShared,
            IAssetsSnmpDataSharedService assetsSnmpDataShared,
            IAssetSnmpDataService assetSnmpDataService,
            IAssetsHistoricalDataSharedService assetsHistoricalDataShared)
        {
            this._logger = logger;
            this._scopeFactory = scopeFactory;
            this._assetsLiveDataShared = assetsLiveDataShared;
            this._assetsSnmpDataShared = assetsSnmpDataShared;
            this._assetSnmpDataService = assetSnmpDataService;
            this._assetsHistoricalDataShared = assetsHistoricalDataShared;
        }

        public async Task<AssetsDataReply> GetAssetsData(AssetsDataRequest request, CallContext context = default)
        {
            _logger.LogInformation($"Assets data request from {context.ServerCallContext.Peer}");
            if (_assetsLiveDataShared.AssetsDataNewConfiguration)
            {
                _logger.LogInformation($"Configuration update flag reply to {context.ServerCallContext.Peer}");
                return await Task.FromResult(new AssetsDataReply() { ConfigurationUpdate = true });
            }

            var reply = new AssetsDataReply() { ConfigurationUpdate = false };
            var assetsDataMessages = new List<AssetDataMessage>();
            foreach (var asset in _assetsLiveDataShared.AssetsData)
            {
                var dataMessage = new AssetDataMessage() { AssetId = asset.Id };
                var tags = new List<AssetTagMessage>();
                foreach (var tag in asset.Data)
                {
                    tags.Add(new AssetTagMessage()
                    {
                        TagId = tag.Id,
                        Value = ByteConverterHelper.ObjectToByteArray(tag.Value.Value),
                        InAlarm = tag.InAlarm
                    }) ;
                }
                dataMessage.Tags = tags;
                assetsDataMessages.Add(dataMessage);
            }

            reply.AssetsData = assetsDataMessages;
            return await Task.FromResult(reply);
        }

        public async Task<AssetsDataConfigurationReply> GetAssetsDataConfiguration(AssetsDataConfigurationRequest request, CallContext context = default)
        {
            _logger.LogInformation($"Assets data configuration request from {context.ServerCallContext.Peer}");

            if (request.NewConfigurationLoaded)
            {
                _logger.LogInformation($"Configuration loaded on client side {context.ServerCallContext.Peer}");
                _assetsLiveDataShared.AssetsDataNewConfigurationClear();
                return await Task.FromResult(new AssetsDataConfigurationReply());
            }
            
            var reply = new AssetsDataConfigurationReply();
            var assetsDataMessages = new List<AssetDataConfigurationMessage>();
            foreach (var asset in _assetsLiveDataShared.AssetsData)
            {
                var dataMessage = new AssetDataConfigurationMessage()
                {
                    AssetId = asset.Id,
                    Name = asset.Name,
                    IPAddress = asset.IPAddress
                };
                var tags = new List<AssetTagConfigurationMessage>();
                foreach (var tag in asset.Data)
                {
                    tags.Add(new AssetTagConfigurationMessage()
                    {
                        TagId = tag.Id,
                        Tagname = tag.Tagname,
                        DataType = (int)tag.ValueDataType,
                        RangeMax = tag.RangeMax,
                        RangeMin = tag.RangeMin
                    });
                }
                dataMessage.Tags = tags;
                assetsDataMessages.Add(dataMessage);
            }

            reply.AssetsData = assetsDataMessages;
            return await Task.FromResult(reply);
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
