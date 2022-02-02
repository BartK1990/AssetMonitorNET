using AssetMonitorService.Monitor.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services
{
    public class AssetPingDataService : IAssetPingService
    {
        private readonly ILogger<AssetPingDataService> _logger;

        public AssetPingDataService(ILogger<AssetPingDataService> logger)
        {
            this._logger = logger;
        }

        public async Task UpdateAsset(AssetPing assetPing)
        {
            var pData = await PingHostAsync(assetPing.IpAddress);

            assetPing.PingState = pData.PingState;
            assetPing.PingResponseTime = pData.RoundtripTime;
        }

        private async Task<PingHostReturnData> PingHostAsync(string hostname)
        {
            var pingData = new PingHostReturnData();
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = await pinger.SendPingAsync(hostname);
                pingData.PingState = reply.Status == IPStatus.Success;
                pingData.RoundtripTime = reply.RoundtripTime;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Cannot retrieve data from Agent: {hostname}");
                _logger.LogDebug($"Exception: {ex.Message}");
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }
            return pingData;
        }

        private struct PingHostReturnData
        {
            public bool PingState;
            public long RoundtripTime;
        }

    }
}
