using AssetMonitorDataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetMonitorService.Data.Repositories
{
    public interface IAssetMonitorRepository
    {
        Task<IEnumerable<Asset>> GetAllAssetsAsync();
        Task<IEnumerable<Asset>> GetWindowsAssetsAsync();
        Task<Asset> GetAssetByIdAsync(int? id);
        Task<bool> SaveAllAsync();
    }
}