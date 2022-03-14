using AssetMonitorService.Monitor.Model.Live;
using AssetMonitorService.Monitor.SingletonServices.Alarm;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.Model.Alarm
{
    public class AssetAlarmData
    {
        public int Id { get; private set; }

        public AssetAlarmData(IDictionary<AlarmTagInfo, TagValue> tags, int assetId)
        {
            Data = new Dictionary<TagAlarmValue, TagValue>();
            foreach (var keyValue in tags)
            {
                Data.Add(
                    new TagAlarmValue(tagname: keyValue.Value.Tagname, dataType: keyValue.Value.DataType,
                    alarmType: keyValue.Key.AlarmType, alarmValue: keyValue.Key.AlarmValue, activationTime: keyValue.Key.ActivationTime,
                    description: keyValue.Key.Description),
                    keyValue.Value);
            }
            this.Id = assetId;
        }

        public IDictionary<TagAlarmValue, TagValue> Data { get; set; }

        public void UpdateData()
        {
            var keys = new List<TagAlarmValue>();
            keys.AddRange(Data.Keys);
            foreach (var key in keys)
            {
                key.Value = Data[key].Value;
            }
        }
    }
}
