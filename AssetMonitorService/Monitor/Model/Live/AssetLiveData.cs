using AssetMonitorService.Monitor.Model.TagConfig;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.Model.Live
{
    public class AssetLiveData
    {
        public AssetLiveData(ICollection<TagLive> tags, int assetId, string name, string ipAddress)
        {
            Data = tags;
            this.Id = assetId;
            this.Name = name;
            this.IPAddress = ipAddress;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string IPAddress { get; set; }

        public ICollection<TagLive> Data { get; set; }
    }
}
