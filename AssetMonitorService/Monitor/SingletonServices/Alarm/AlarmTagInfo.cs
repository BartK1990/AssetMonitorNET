using AssetMonitorDataAccess.Models.Enums;

namespace AssetMonitorService.Monitor.SingletonServices.Alarm
{
    public class AlarmTagInfo
    {
        public string AlarmValue { get; private set; }
        public int ActivationTime { get; private set; }
        public string Description { get; private set; }
        public AlarmTypeEnum AlarmType { get; private set; }

        public AlarmTagInfo(string alarmValue, int activationTime, string description, AlarmTypeEnum alarmType)
        {
            this.AlarmValue = alarmValue;
            this.ActivationTime = activationTime;
            this.Description = description;
            this.AlarmType = alarmType;
        }
    }
}
