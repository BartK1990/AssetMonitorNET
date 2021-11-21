using AspMVC_Monitor.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Models
{
    public class AssetHolder : IAssetHolder
    {
        public List<Asset> AssetList { get; set; }

        public AssetHolder()
        {
            AssetList = new List<Asset>();
        }

        public void AddAsset(string name, string ipAddress)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(ipAddress))
                return;

            if (!IPAddress.TryParse(ipAddress, out _))
                return;

            if (AssetList.Select(n => n.Name).ToList().Contains(name))
                return;

            if (AssetList.Select(n => n.IpAddress).ToList().Contains(ipAddress))
                return;

            AssetList.Add(
                new Asset()
                {
                    Name = name,
                    IpAddress = ipAddress,
                    PingState = false,
                    PingResponseTime = 0
                });
        }

        public void UpdateAssetPing()
        {
            foreach (var a in AssetList)
            {
                a.PingState = PingHelper.PingHostWithTimeLimit(a.IpAddress, out var pingResponseTime, TimeSpan.FromMilliseconds(4000));
                a.PingResponseTime = pingResponseTime;
            }
        }

        public async Task UpdateAssetPingAsync()
        {
            await Task.Run(() => UpdateAssetPing());
        }
    }
}
