using AspMVC_Monitor.gRPC;
using AspMVC_Monitor.Models;
using AssetMonitorSharedGRPC.Server;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Services.SingletonServices
{
    public class AssetsLiveDataShared : IAssetsLiveDataShared
    {
        public const int TcpPort = 9561; // ToDo make configurable in DB

        public List<AssetLiveData> AssetsData = new List<AssetLiveData>();

        private readonly ILogger<AssetsLiveDataShared> _logger;

        public AssetsLiveDataShared(ILogger<AssetsLiveDataShared> logger)
        {
            this._logger = logger;
        }

        public async Task UpdateAssetsLiveData()
        {
            try
            {
                var client = GrpcHelper<IAssetMonitorDataService>.CreateSecureClient(IPAddress.Loopback.ToString(), TcpPort);

                var reply = await client.GetAssetsData(
                    new AssetsDataRequest { Init = 1 });

                foreach (var asset in AssetsList)
                {
                    var assetPingData = reply.AssetsData.Where(a => a.Id == asset.Id).FirstOrDefault();
                    asset.PingState = assetPingData.PingState;
                    asset.PingResponseTime = assetPingData.PingResponseTime;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Cannot retrieve ping data from Asset Monitor service");
                _logger.LogDebug($"Exception: {ex.Message}");
            }
        }
    }
}
