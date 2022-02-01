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
        Task<bool> SaveAllAsync();
    }
}