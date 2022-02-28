using AssetMonitorDataAccess.Models;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.SingletonServices.Historical
{
    public class HistoricalTagInfo
    {
        public string Tagname;
        //public string ColumnNameSuffix;
        public int ValueDataTypeId;
        public ICollection<HistoricalTagConfig> HistoricalTagConfigs;
        public bool IsNull;
    }
}
