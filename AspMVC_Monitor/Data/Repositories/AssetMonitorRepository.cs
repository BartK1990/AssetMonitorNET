using AssetMonitorDataAccess.DataAccess;
using AssetMonitorDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Data.Repositories
{
    public class AssetMonitorRepository : IAssetMonitorRepository
    {
        private readonly AssetMonitorContext _context;

        public AssetMonitorRepository(AssetMonitorContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asset>> GetAllAssetsAsync()
        {
            var assetMonitorContext = _context.Asset.Include(a => a.AssetType);
            return await assetMonitorContext.ToListAsync();
        }

        public async Task<Asset> GetAssetByIdAsync(int? id)
        {
            return await _context.Asset
                .Include(a => a.AssetType)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
