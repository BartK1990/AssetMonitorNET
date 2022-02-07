using AssetMonitorDataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetMonitorService.Data.Repositories
{
    public interface IAssetMonitorRepository
    {
        Task<IEnumerable<Asset>> GetAllAssetsAsync();
        Task<IEnumerable<Asset>> GetAgentAssetsAsync();
        Task<IEnumerable<Asset>> GetAgentAssetsWithTagSetAsync();
        Task<IEnumerable<Asset>> GetSnmpAssetsWithTagSetAsync();
        Task<Asset> GetAssetByIdAsync(int? id);
        Task<Asset> GetAssetPropertiesByIdAsync(int? id);
        Task<IEnumerable<AgentTag>> GetAgentTagsBySetIdAsync(int? setId);
        Task<IEnumerable<AgentTag>> GetAgentTagsWithHistoricalByAssetIdAsync(int? id);
        Task<IEnumerable<SnmpTag>> GetSnmpTagsWithHistoricalByAssetIdAsync(int? id);
        Task<IEnumerable<SnmpTag>> GetSnmpAssetTagsByAssetIdAsync(int? id);
        Task<IEnumerable<SnmpAssetValue>> GetSnmpAssetValuesByAssetIdAsync(int? id);
        Task<bool> SaveAllAsync();
    }
}