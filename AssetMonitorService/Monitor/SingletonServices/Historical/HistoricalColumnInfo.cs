using AssetMonitorDataAccess.Models.Enums;

namespace AssetMonitorService.Monitor.SingletonServices.Historical
{
    public class HistoricalColumnInfo
    {
        public readonly string Name;
        public readonly HistoricalTypeEnum Type;

        public HistoricalColumnInfo(string name, HistoricalTypeEnum type)
        {
            this.Name = name;
            this.Type = type;
        }
    }
}
