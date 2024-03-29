﻿using AssetMonitorAgent.SingletonServices;
using AssetMonitorSharedGRPC.Agent;
using AssetMonitorSharedGRPC.Helpers;
using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetMonitorAgent.CommunicationServices
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

        public async Task<AssetDataReply> GetAssetDataAsync(AssetDataRequest request, CallContext context = default)
        {
            _assetDataSharedService.UpdateConfiguration(request.Tags, request.ScanTime);

            var dataList = new List<AssetDataItemReply>();
            foreach (var r in request.Tags)
            {
                dataList.Add(new AssetDataItemReply() { ByteArray = ByteConverterHelper.ObjectToByteArray(_assetDataSharedService.Data[r]) });
            }
            var reply = new AssetDataReply() { Data = dataList };

            _logger.LogInformation($"Replying to {context.ServerCallContext.Peer}");
            return await Task.FromResult(reply);
        }
    }
}
