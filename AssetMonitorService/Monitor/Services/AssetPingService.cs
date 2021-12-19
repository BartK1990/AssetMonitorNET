using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services
{
    public class AssetPingService : IAssetPingService
    {

        public async Task PingHostAsync(string hostname)
        {
            bool pingable = false;
            Ping pinger = null;
            var pingTime = 0L;

            try
            {
                pinger = new Ping();
                PingReply reply = await pinger.SendPingAsync(hostname);
                pingable = reply.Status == IPStatus.Success;
                pingTime = reply.RoundtripTime;
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
        }

    }
}
