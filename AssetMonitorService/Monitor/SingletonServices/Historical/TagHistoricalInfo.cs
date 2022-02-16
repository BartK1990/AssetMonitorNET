using AssetMonitorDataAccess.Models;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.SingletonServices.Historical
{
    public class TagHistoricalInfo
    {
        public string Tagname;
        public int ValueDataTypeId;
        public ICollection<HistoricalTagConfig> HistoricalTagConfigs;
        public bool IsNull;
    }
}
