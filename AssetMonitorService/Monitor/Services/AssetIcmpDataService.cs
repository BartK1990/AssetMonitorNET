using AssetMonitorService.Monitor.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services
{
    public class AssetIcmpDataService : IAssetIcmpDataService
    {
        private readonly ILogger<AssetIcmpDataService> _logger;

        public AssetIcmpDataService(ILogger<AssetIcmpDataService> logger)
        {
            this._logger = logger;
        }

        public async Task UpdateAsset(AssetIcmpData assetPing)
        {
            if(!assetPing.Data?.Any() ?? true)
            {
                _logger.LogWarning($"Service {this.GetType().Name} has no data tags to ping");
                return;
            }

            var pData = await PingHostAsync(assetPing.IpAddress);

            foreach (var tag in assetPing.Data)
            {
                switch (tag.Key.IcmpType)
                {
                    case AssetMonitorDataAccess.Models.Enums.IcmpTypeEnum.PingState:
                        tag.Value.Value = pData.PingState;
                        break;
                    case AssetMonitorDataAccess.Models.Enums.IcmpTypeEnum.PingResponseTime:
                        tag.Value.Value = pData.RoundtripTime;
                        break;
                }
            }
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
