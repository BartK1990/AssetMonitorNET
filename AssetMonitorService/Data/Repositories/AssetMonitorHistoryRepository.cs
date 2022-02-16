using AssetMonitorHistoryDataAccess.DataAccess;
using AssetMonitorHistoryDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetMonitorService.Data.Repositories
{
    public class AssetMonitorHistoryRepository : IAssetMonitorHistoryRepository
    {
        private readonly AssetMonitorHistoryContext _context;

        public AssetMonitorHistoryRepository(AssetMonitorHistoryContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<HistoricalDataTable>> GetAllTablesAsync()
        {
            var assetMonitorContext = _context.HistoricalDataTable;
            return await assetMonitorContext.ToListAsync();
        }

        public void Add(object entity)
        {
            _context.Add(entity);
        }

        public void Update(object entity)
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
