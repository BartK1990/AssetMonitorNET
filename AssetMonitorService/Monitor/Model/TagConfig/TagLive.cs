using AssetMonitorDataAccess.Models;
using AssetMonitorService.Monitor.Model.Alarm;
using AssetMonitorService.Monitor.Model.Live;
using System.Collections.Generic;
using System.Linq;

namespace AssetMonitorService.Monitor.Model.TagConfig
{
    public class TagLive : TagConfigBase
    {
        public TagLive()
        {
        }
#nullable enable
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

        public TagValue Value { get; private set; }

        public bool InAlarm
        {
            get
            {
                var inAlarm = false;

                if(TagAlarmValues?.Any() ?? false)
                {
                    foreach (var alarm in TagAlarmValues)
                    {
                        if(alarm.AlarmState == true)
                        {
                            inAlarm = true;
                        }
                    }
                }
                return inAlarm;
            }
        }

        public ICollection<TagAlarmValue>? TagAlarmValues { get; private set; }

        public double? RangeMax { get; private set; }
        public double? RangeMin { get; private set; }
#nullable disable
    }
}
