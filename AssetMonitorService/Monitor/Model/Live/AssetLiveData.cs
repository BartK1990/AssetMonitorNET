using AssetMonitorService.Monitor.Model.TagConfig;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.Model.Live
{
    public class AssetLiveData
    {
        public AssetLiveData(ICollection<TagLive> tags, int assetId)
        {
            Data = tags;
            this.Id = assetId;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TagLive> Data { get; set; }
    }
}
