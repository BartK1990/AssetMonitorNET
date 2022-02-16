using AssetMonitorDataAccess.DataAccess;
using AssetMonitorDataAccess.Models;
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
            this._context = context;
        }

        public async Task<IEnumerable<Asset>> GetAllAssetsAsync()
        {
            var assetMonitorContext = _context.Asset;
            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<Asset>> GetAgentAssetsAsync()
        {
            var assetMonitorContext = _context.Asset
                .Where(at => at.AgentTagSet != null);

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<Asset>> GetAgentAssetsWithTagSetAsync()
        {
            var assetMonitorContext = _context.Asset
                .Include(at=>at.AgentTagSet).ThenInclude(ats=>ats.AgentTag)
                .Where(at => at.AgentTagSet != null);

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<Asset>> GetSnmpAssetsWithTagSetAsync()
        {
            var assetMonitorContext = _context.Asset
                .Include(st => st.SnmpTagSet).ThenInclude(sts => sts.SnmpTag)
                .Include(st => st.SnmpTagSet).ThenInclude(stv => stv.Version)
                .Where(at => at.SnmpTagSet != null);

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<Asset> GetAssetByIdAsync(int? id)
        {
            return await _context.Asset
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Asset> GetAssetPropertiesByIdAsync(int? id)
        {
            return await _context.Asset
                .Include(p=>p.AssetPropertyValues)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<AgentTag>> GetAgentTagsBySetIdAsync(int? setId)
        {
            var assetMonitorContext = _context.AgentTag
                .Where(at => at.AgentTagSetId == setId);

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<AgentTag>> GetAgentTagsWithHistoricalByAssetIdAsync(int? id)
        {
            var assetMonitorContext = _context.AgentTag
                .Where(at => at.AgentTagSetId == (_context.Asset.Where(a => a.Id == id).FirstOrDefault().AgentTagSetId))
                .Include(h => h.HistorizationTagConfigs);

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<SnmpTag>> GetSnmpTagsWithHistoricalByAssetIdAsync(int? id)
        {
            var assetMonitorContext = _context.SnmpTag
                .Where(at => at.SnmpTagSetId == (_context.Asset.Where(a => a.Id == id).FirstOrDefault().SnmpTagSetId))
                .Include(h => h.HistorizationTagConfigs);

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<SnmpTag>> GetSnmpAssetTagsByAssetIdAsync(int? id)
        {
            var assetMonitorContext = _context.SnmpTag
                .Where(st => st.SnmpTagSetId == 
                    _context.Asset.Where(a=>a.Id == id).Select(a=>a.SnmpTagSetId).FirstOrDefault()
                );

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<SnmpAssetValue>> GetSnmpAssetValuesByAssetIdAsync(int? id)
        {
            var assetMonitorContext = _context.SnmpAssetValue
                .Where(sa => sa.AssetId == id);

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
