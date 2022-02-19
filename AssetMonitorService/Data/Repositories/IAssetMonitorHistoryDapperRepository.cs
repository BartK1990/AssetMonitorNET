using AssetMonitorService.Data.Repositories.Historical;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetMonitorService.Data.Repositories
{
    public interface IAssetMonitorHistoryDapperRepository
    {
        public Task<string> GetDbVersionAsync();
        Task CreateOrUpdateTableAsync(string tableName, IList<TableColumnConfig> columns);
        Task CreateOrUpdateTimeSeriesTableAsync(string tableName, IList<TableColumnConfig> columns);
        Task InsertToTableAsync(string tableName, IList<TableColumnValue> columns);
        Task InsertToTimeSeriesTableAsync(string tableName, IList<TableColumnValue> columns, string timeStamp);
    }
}
