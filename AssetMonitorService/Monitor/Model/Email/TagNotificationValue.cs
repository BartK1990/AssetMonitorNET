using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Model.Email
{
#nullable enable
    public class TagNotificationValue : TagBase
    {
        public TagNotificationValue(string tagname) : base(tagname: tagname)
        {
            _lastAlarmState = true; // To not get new notification immiedietly after initialization
        }

        private bool _lastAlarmState;

        public bool AlarmState { get; private set; }

        //
        // Summary:
        // Returns true when new notification for this value should be created
        public bool CheckIfNotificationAndPutNewValue(bool alarmState)
        {
            bool result = false;
            AlarmState = alarmState;

            if(AlarmState && (!_lastAlarmState))
            {
                result = true;
            }

            _lastAlarmState = AlarmState;

            return result;
        }
    }
}
