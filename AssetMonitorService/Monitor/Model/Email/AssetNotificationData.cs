using AssetMonitorService.Monitor.Model.Alarm;
using System.Collections.Generic;
using System.Linq;

namespace AssetMonitorService.Monitor.Model.Email
{
    public class AssetNotificationData
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public bool Enabled { get; private set; }

        public AssetNotificationData(ICollection<TagAlarmValue> tags, ICollection<EmailAddress> recipients, int assetId, string assetName, bool enabled)
        {
            this.Data = new Dictionary<TagAlarmValue, TagNotificationValue>();
            this.Recipients = recipients;
            foreach (var tag in tags)
            {
                Data.Add(tag, new TagNotificationValue(tag.Tagname));
            }
            this.Id = assetId;
            this.Name = assetName;
            this.Enabled = enabled;
        }

        public IDictionary<TagAlarmValue, TagNotificationValue> Data { get; private set; }
        public ICollection<EmailAddress> Recipients { get; private set; }

#nullable enable
        public ICollection<string>? GetAlarmMessage()
        {
            if (!Enabled)
            {
                return null;
            }

            var messages = new List<string>();
            int LineNumber = 1;
            foreach (var keyValue in Data)
            {
                if (keyValue.Value.CheckIfNotificationAndPutNewValue(keyValue.Key.AlarmState))
                {
                    if(keyValue.Key.Value != null)
                    {
                        var message = $@"{LineNumber++}) {keyValue.Key.Description} | Value:[{keyValue.Key.Value}] Alarm Value:[{keyValue.Key.AlarmValue}]";
                        messages.Add(message);
                    }
                }
            }

            if (messages.Any())
            {
                messages.Insert(0, $@"Asset Id: [{Id}], Asset Name : [{Name}] Active alarms:");
                return messages;
            }
            else
            {
                return null;
            }
        }
#nullable disable
    }
}
