using AssetMonitorDataAccess.Models;
using AssetMonitorService.Monitor.Model.Alarm;
using AssetMonitorService.Monitor.Model.Live;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.Model.TagConfig
{
    public class TagLive : TagConfigBase
    {
        public TagLive()
        {
        }

        public TagLive(Tag tag, TagValue val, ICollection<TagAlarmValue>? tagAlarmValues, double? rangeMin, double? rangeMax) : base(tag)
        {
            if (rangeMin != null && rangeMax != null)
            {
                this.RangeMin = rangeMin;
                this.RangeMax = rangeMax;
            }
            this.Value = val;
            this.TagAlarmValues = TagAlarmValues;
        }

        public TagValue Value { get; set; }
#nullable enable
        public ICollection<TagAlarmValue>? TagAlarmValues { get; set; }

        public double? RangeMax { get; set; }
        public double? RangeMin { get; set; }
#nullable disable
    }
}
