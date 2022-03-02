using AssetMonitorService.Monitor.Model.Live;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.Model.Historical
{
    public class AssetHistoricalData
    {
        public int Id { get; private set; }

        public AssetHistoricalData(ICollection<TagValue> tags, int dataWindowSize, int assetId)
        {
            Data = new Dictionary<TagHistoricalValue, TagValue>();
            foreach (var tag in tags)
            {
                Data.Add(new TagHistoricalValue(tag.Tagname, tag.DataType, dataWindowSize), tag);
            }
            this.Id = assetId;
        }

        public IDictionary<TagHistoricalValue, TagValue> Data { get; set; }

        public void UpdateData()
        {
            foreach (var keyValuePair in Data)
            {
                keyValuePair.Key.ValueBufferEnqueue(keyValuePair.Value.Value);
            }
        }
    }
}
