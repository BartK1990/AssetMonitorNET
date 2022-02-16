using AssetMonitorHistoryDataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetMonitorService.Data.Repositories
{
    public interface IAssetMonitorHistoryRepository
    {
        Task<IEnumerable<HistoricalDataTable>> GetAllTablesAsync();
        void Add(object entity);
        void Update(object entity);
        Task<bool> SaveAllAsync();
    }
}
