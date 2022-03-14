using AssetMonitorDataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetMonitorService.Data.Repositories
{
    public interface IAssetMonitorRepository
    {
        Task<IEnumerable<ApplicationProperty>> GetAppPropertiesAsync();
        Task<IEnumerable<Asset>> GetAllAssetsAsync();
        Task<IEnumerable<Asset>> GetIcmpAssetsAsync();
        Task<IEnumerable<Asset>> GetAgentAssetsAsync();
        Task<IEnumerable<Asset>> GetSnmpAssetsAsync();
        Task<Asset> GetAssetByIdAsync(int id);
        Task<Asset> GetAssetPropertiesByIdAsync(int id);
        Task<IEnumerable<Tag>> GetTagsIncludeAlarmByAssetIdAsync(int id);
        Task<IEnumerable<Tag>> GetIcmpTagsByAssetIdAsync(int id);
        Task<IEnumerable<Tag>> GetAgentTagsByAssetIdAsync(int id);
        Task<IEnumerable<Tag>> GetSnmpTagsByAssetIdAsync(int id);
        Task<IEnumerable<Tag>> GetIcmpTagsBySetIdAsync(int setId);
        Task<IEnumerable<Tag>> GetAgentTagsBySetIdAsync(int setId);
        Task<IEnumerable<Tag>> GetSnmpTagsBySetIdAsync(int setId);
        Task<IEnumerable<Tag>> GetTagsWithHistoricalByAssetIdAsync(int id);
        Task<IEnumerable<Tag>> GetTagsWithAlarmByAssetIdAsync(int id);
        Task<IEnumerable<SnmpAssetValue>> GetSnmpAssetValuesByAssetIdAsync(int id);
        Task<IEnumerable<UserEmailAddress>> GetUserEmailAddressByAssetIdAsync(int id);
        Task<AssetTagRange> GetAssetTagRangeByAssetIdTagIdAsync(int assetId, int tagId);
        void Add(object entity);
        void Update(object entity);
        Task<bool> SaveAllAsync();
    }
}