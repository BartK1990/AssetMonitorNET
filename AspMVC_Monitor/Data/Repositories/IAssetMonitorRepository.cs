using AssetMonitorDataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Data.Repositories
{
    public interface IAssetMonitorRepository
    {
        Task<IEnumerable<Asset>> GetAllAssetsAsync();
        Task<Asset> GetAssetByIdAsync(int? id);
        Task<bool> SaveAllAsync();
    }
}