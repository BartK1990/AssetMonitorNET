using AspMVC_Monitor.Models;

namespace AspMVC_Monitor.Controllers.Json
{
    public class assetTagJson
    {
        public assetTagJson()
        {
        }

        public assetTagJson(TagLiveValue tag, int sharedId)
        {
            sharedTagId = sharedId;
            id = tag.Id;
            tagname = tag.Tagname;
            dataType = tag.DataType.ToString();
            value = tag.Value;
            inAlarm = tag.InAlarm;
            rangeMax = tag.RangeMax;
            rangeMin = tag.RangeMin;
        }

        public int id { get; set; }
        public int sharedTagId { get; set; }
        public string tagname { get; set; }
        public string dataType { get; set; }
        public object value { get; set; }
        public bool inAlarm { get; set; }
        public double? rangeMax { get; set; }
        public double? rangeMin { get; set; }
    }
}
