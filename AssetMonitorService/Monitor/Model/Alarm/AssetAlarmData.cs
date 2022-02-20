using AssetMonitorService.Monitor.SingletonServices.Alarm;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.Model.Alarm
{
    public class AssetAlarmData
    {
        public int Id { get; private set; }

        public AssetAlarmData(IDictionary<TagValue, AlarmTagInfo> tags, int assetId)
        {
            Data = new Dictionary<TagValue, TagAlarmValue>();
            foreach (var keyValue in tags)
            {
                Data.Add(keyValue.Key, new TagAlarmValue(tagname: keyValue.Key.Tagname, dataType: keyValue.Key.DataType, 
                    alarmType: keyValue.Value.AlarmType, alarmValue: keyValue.Value.AlarmValue, activationTime: keyValue.Value.ActivationTime,
                    description: keyValue.Value.Description));
            }
            this.Id = assetId;
        }

        public IDictionary<TagValue, TagAlarmValue> Data { get; set; }

        public void UpdateData()
        {
            var keys = new List<TagValue>();
            keys.AddRange(Data.Keys);
            foreach (var key in keys)
            {
                Data[key].Value = key.Value;
            }
        }
    }
}
