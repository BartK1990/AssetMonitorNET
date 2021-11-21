using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace AspMVC_Monitor.Models.Helpers
{
    public static class PingHelper
    {
        public static bool PingHost(string nameOrAddress, out long pingTime)
        {
            bool pingable = false;
            Ping pinger = null;
            pingTime = 0;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
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

            return pingable;
        }

        public static bool PingHostWithTimeLimit(string nameOrAddress, out long pingTime, TimeSpan timeLimit)
        {
            bool pingable = false;
            long pingTimeLocal = 0;
            bool Completed = ExecuteHelper.ExecuteWithTimeLimit(timeLimit, () =>
            {
                pingable = PingHost(nameOrAddress, out var pTime);
                pingTimeLocal = pTime;
            });

            pingTime = pingTimeLocal;
            return pingable;
        }
    }
}
