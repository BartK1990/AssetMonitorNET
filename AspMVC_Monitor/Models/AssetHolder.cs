using System.Collections.Generic;
using System.Net;

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

            if (IPAddress.TryParse(ipAddress, out _))
            {
                AssetList.Add(
                    new Asset() { Name = name, IpAddress = ipAddress, pingState = false });
            }
        }
    }
}
