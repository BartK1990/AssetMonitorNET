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
            var assetMonitorContext = _context.Asset.Include(a => a.AssetType);
            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<Asset>> GetAgentAssetsAsync()
        {
            var assetMonitorContext = _context.Asset
                .Where(at => at.AgentTagSet != null);

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<Asset>> GetAgentAssetsWithPropertiesAndTagSetAsync()
        {
            var assetMonitorContext = _context.Asset
                .Include(p => p.AssetPropertyValues)
                .Include(at=>at.AgentTagSet).ThenInclude(ats=>ats.AgentTag)
                .Where(at => at.AgentTagSet != null);

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<Asset> GetAssetByIdAsync(int? id)
        {
            return await _context.Asset
                .Include(a => a.AssetType)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<AgentTag>> GetAgentTagsBySetIdAsync(int? setId)
        {
            var assetMonitorContext = _context.AgentTag
                .Where(at => at.AgentTagSetId == setId);

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
