using AssetMonitorDataAccess.Models.Enums;

namespace AssetMonitorService.Monitor.Model
{
    public class AssetPing
    {
        public int Id { get; set; }

        public string IpAddress { get; set; }

        private bool _pingState;
        public bool PingState 
        {
            get { return _pingState; }
            set 
            {
                PingStateValue.Value = value;
                _pingState = value;
            }
        }

        private long _pingResponseTime;
        public long PingResponseTime
        {
            get { return _pingResponseTime; }
            set
            {
                PingResponseTimeValue.Value = value;
                _pingResponseTime = value;
            }
        }

        public TagValue PingStateValue { get; private set; }
        public TagValue PingResponseTimeValue { get; private set; }

        public AssetPing()
        {
            this.PingStateValue = new TagValue("PingState", TagDataTypeEnum.Boolean, 1.0, 0.0);
            this.PingResponseTimeValue = new TagValue("PingResponseTime", TagDataTypeEnum.Long, 1.0, 0.0);
        }
    }
}
