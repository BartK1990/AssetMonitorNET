using System.Collections.Generic;

namespace AssetMonitorService.Monitor.Model
{
    public class AssetIcmpData
    {
        public int Id { get; set; }

        public string IpAddress { get; set; }

        public AssetIcmpData(ICollection<TagIcmp> tags)
        {
            Data = new Dictionary<TagIcmp, TagValue>(new TagIcmp());
            foreach (var tag in tags)
            {
                Data.Add(tag, new TagValue(tag.Tagname, tag.ValueDataType, tag.ScaleFactor, tag.ScaleOffset));
            }
        }

        public IDictionary<TagIcmp, TagValue> Data { get; set; }
    }
}
