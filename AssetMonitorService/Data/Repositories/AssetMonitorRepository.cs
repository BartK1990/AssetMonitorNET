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

        public async Task<IEnumerable<Asset>> GetIcmpAssetsAsync()
        {
            var IcmpTagSets = _context.Tag
                .Include(tc => tc.TagCommunicationRel)
                .Where(t => t.TagCommunicationRel.IcmpTagId != null);

            var assetMonitorContext = _context.Asset
                .Where(t => IcmpTagSets.Any(ts => ts.TagSetId == t.TagSetId));

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<Asset>> GetAgentAssetsAsync()
        {
            var AgentTagSets = _context.Tag
                .Include(tc => tc.TagCommunicationRel)
                .Where(t => t.TagCommunicationRel.AgentTagId != null);

            var assetMonitorContext = _context.Asset
                .Where(t => AgentTagSets.Any(ts => ts.TagSetId == t.TagSetId));

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<Asset>> GetSnmpAssetsAsync()
        {
            var SnmpTagSets = _context.Tag
                .Include(tc => tc.TagCommunicationRel)
                .Where(t => t.TagCommunicationRel.SnmpTagId != null);

            var assetMonitorContext = _context.Asset
                .Where(t => SnmpTagSets.Any(ts => ts.TagSetId == t.TagSetId));

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<Asset> GetAssetByIdAsync(int id)
        {
            return await _context.Asset
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Asset> GetAssetPropertiesByIdAsync(int id)
        {
            return await _context.Asset
                .Include(p=>p.AssetPropertyValues)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Tag>> GetIcmpTagSetByAssetIdAsync(int id)
        {
            var assetMonitorContext = _context.Tag
                .Include(tc => tc.TagCommunicationRel).ThenInclude(tt => tt.IcmpTag)
                .Where(t => t.TagSetId == _context.Asset.FirstOrDefault(a => a.Id == id).TagSetId);

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetAgentTagSetByAssetIdAsync(int id)
        {
            var assetMonitorContext = _context.Tag
                .Include(tc => tc.TagCommunicationRel).ThenInclude(tt => tt.AgentTag)
                .Where(t => t.TagSetId == _context.Asset.FirstOrDefault(a => a.Id == id).TagSetId);

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetSnmpTagSetByAssetIdAsync(int id)
        {
            var assetMonitorContext = _context.Tag
                .Include(tc => tc.TagCommunicationRel).ThenInclude(tt => tt.SnmpTag)
                .Where(t => t.TagSetId == _context.Asset.FirstOrDefault(a => a.Id == id).TagSetId);

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetIcmpTagsBySetIdAsync(int setId)
        {
            var assetMonitorContext = _context.Tag
                .Include(tc => tc.TagCommunicationRel).ThenInclude(tt => tt.IcmpTag)
                .Where(t => t.TagSetId == setId);

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetAgentTagsBySetIdAsync(int setId)
        {
            var assetMonitorContext = _context.Tag
                .Include(tc => tc.TagCommunicationRel).ThenInclude(tt => tt.AgentTag)
                .Where(t => t.TagSetId == setId);

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetSnmpTagsBySetIdAsync(int setId)
        {
            var assetMonitorContext = _context.Tag
                .Include(tc => tc.TagCommunicationRel).ThenInclude(tt => tt.SnmpTag)
                .Where(t => t.TagSetId == setId);

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetTagsWithHistoricalByAssetIdAsync(int id)
        {
            var assetMonitorContext = _context.Tag
                .Where(t => t.TagSetId == _context.Asset.Where(a => a.Id == id).FirstOrDefault().TagSetId)
                .Include(h => h.HistoricalTagConfigs);

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetTagsWithAlarmByAssetIdAsync(int id)
        {
            var assetMonitorContext = _context.Tag
                .Where(t => t.TagSetId == _context.Asset.Where(a => a.Id == id).FirstOrDefault().TagSetId)
                .Include(a => a.AlarmTagConfigs);

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<SnmpAssetValue>> GetSnmpAssetValuesByAssetIdAsync(int id)
        {
            var assetMonitorContext = _context.SnmpAssetValue
                .Where(sa => sa.AssetId == id);

            return await assetMonitorContext.ToListAsync();
        }

        public async Task<IEnumerable<UserEmailAddress>> GetUserEmailAddressByAssetIdAsync(int id)
        {
            var assetMonitorContext = _context.UserEmailAddress
                .Where(sa => sa.UserEmailAddressSetId == (_context.UserEmailAssetRel.FirstOrDefault(a=>a.AssetId == id).UserEmailAddressSetId));

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
