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
            var assetMonitorContext = _context.Asset;
            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<ApplicationProperty>> GetAppPropertiesAsync()
        {
            return await _context.ApplicationProperty
                .Include(v => v.ApplicationPropertyValue)
                .Include(dt => dt.ValueDataType).ToListAsync();
        }

        public async Task<Asset> GetAssetByIdAsync(int? id)
        {
            return await _context.Asset
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<TagSharedSet>> GetAllTagSharedSetsAsync()
        {
            return await _context.TagSharedSet.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
