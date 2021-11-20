using System.Collections.Generic;

namespace AspMVC_Monitor.Models
{
    public interface IAssetHolder
    {
        List<Asset> AssetList { get; set; }

        void AddAsset(string name, string ipAddress);
    }
}
