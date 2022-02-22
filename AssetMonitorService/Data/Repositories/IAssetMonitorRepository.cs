using AssetMonitorDataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetMonitorService.Data.Repositories
{
    public interface IAssetMonitorRepository
    {
        Task<IEnumerable<Asset>> GetAllAssetsAsync();
        Task<IEnumerable<Asset>> GetIcmpAssetsAsync();
        Task<IEnumerable<Asset>> GetAgentAssetsAsync();
        Task<IEnumerable<Asset>> GetSnmpAssetsAsync();
        Task<IEnumerable<Asset>> GetAgentAssetsWithTagSetAsync();
        Task<IEnumerable<Asset>> GetSnmpAssetsWithTagSetAsync();
        Task<Asset> GetAssetByIdAsync(int? id);
        Task<Asset> GetAssetPropertiesByIdAsync(int? id);
        Task<IEnumerable<AgentTag>> GetAgentTagsBySetIdAsync(int? setId);
        Task<IEnumerable<AgentTag>> GetAgentTagsWithHistoricalByAssetIdAsync(int? id);
        Task<IEnumerable<SnmpTag>> GetSnmpTagsWithHistoricalByAssetIdAsync(int? id);
        Task<IEnumerable<SnmpTag>> GetSnmpAssetTagsByAssetIdAsync(int? id);
        Task<IEnumerable<SnmpAssetValue>> GetSnmpAssetValuesByAssetIdAsync(int? id);
        Task<IEnumerable<IcmpTag>> GetIcmpTagsWithAlarmByAssetIdAsync(int? id);
        Task<IEnumerable<AgentTag>> GetAgentTagsWithAlarmByAssetIdAsync(int? id);
        Task<IEnumerable<SnmpTag>> GetSnmpTagsWithAlarmByAssetIdAsync(int? id);
        Task<IEnumerable<UserEmailAddress>> GetUserEmailAddressByAssetIdAsync(int? id);
        void Add(object entity);
        void Update(object entity);
        Task<bool> SaveAllAsync();
    }
}