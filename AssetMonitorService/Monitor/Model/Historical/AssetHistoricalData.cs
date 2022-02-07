using AssetMonitorDataAccess.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Model.Historical
{
    public class AssetHistoricalData
    {
        public AssetHistoricalData(ICollection<TagValue> tags)
        {
            Data = new Dictionary<TagValue, TagHistoricalValue>();
            foreach (var tag in tags)
            {
                Data.Add(tag, new TagHistoricalValue(tag.DataType, HistoricalTypeEnum.Last ,16));
            }
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
