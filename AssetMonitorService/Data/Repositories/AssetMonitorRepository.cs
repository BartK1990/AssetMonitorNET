using AssetMonitorDataAccess.DataAccess;
using AssetMonitorDataAccess.Models;
using AssetMonitorDataAccess.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.Data.Repositories
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
            var assetMonitorContext = _context.Assets.Include(a => a.AssetType);
            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<Asset>> GetWindowsAssetsAsync()
        {
            var assetMonitorContext = _context.Assets.Include(a => a.AssetType)
                .Where(at => at.AssetType.Type == AssetTypeEnum.Windows.ToString());
            return await assetMonitorContext.ToListAsync();
        }

        public async Task<Asset> GetAssetByIdAsync(int? id)
        {
            return await _context.Assets
                .Include(a => a.AssetType)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
