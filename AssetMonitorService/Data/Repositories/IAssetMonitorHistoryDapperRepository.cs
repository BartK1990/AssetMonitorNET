using AssetMonitorService.Data.Repositories.Historical;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetMonitorService.Data.Repositories
{
    public interface IAssetMonitorHistoryDapperRepository
    {
        public Task<string> GetDbVersion();
        Task CreateOrUpdateTable(string tableName, IList<TableColumnConfig> columns);
        Task CreateOrUpdateTimeSeriesTable(string tableName, IList<TableColumnConfig> columns);
        Task InsertToTable(string tableName, IList<TableColumnValue> columns);
        Task InsertToTimeSeriesTable(string tableName, IList<TableColumnValue> columns, string timeStamp);
    }
}
