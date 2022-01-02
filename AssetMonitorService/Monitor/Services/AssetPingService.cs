using AssetMonitorService.Monitor.Model;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services
{
    public class AssetPingService : IAssetPingService
    {

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
            catch (PingException)
            {
                // Discard Ping Exceptions and return false;
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
