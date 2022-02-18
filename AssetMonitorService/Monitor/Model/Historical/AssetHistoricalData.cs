using System.Collections.Generic;

namespace AssetMonitorService.Monitor.Model.Historical
{
    public class AssetHistoricalData
    {
        public int Id { get; private set; }

        public AssetHistoricalData(ICollection<TagValue> tags, int dataWindowSize, int assetId)
        {
            Data = new Dictionary<TagValue, TagHistoricalValue>();
            foreach (var tag in tags)
            {
                Data.Add(tag, new TagHistoricalValue(tag.Tagname, tag.DataType, dataWindowSize));
            }
            this.Id = assetId;
        }

        public IDictionary<TagValue, TagHistoricalValue> Data { get; set; }

        public void UpdateData()
        {
            foreach (var keyValuePair in Data)
            {
                keyValuePair.Value.ValueBufferEnqueue(keyValuePair.Key.Value);
            }
        }
    }
}
