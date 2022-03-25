using AssetMonitorDataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Data.Repositories
{
    public interface IAssetMonitorRepository
    {
        Task<IEnumerable<ApplicationProperty>> GetAppPropertiesAsync();
        Task<IEnumerable<Asset>> GetAllAssetsAsync();
        Task<Asset> GetAssetByIdAsync(int? id);
        Task<IEnumerable<TagSharedSet>> GetAllTagSharedSetsAsync();
        Task<IEnumerable<TagShared>> GetTagSharedBySetIdAsync(int SetId);
        Task<bool> SaveAllAsync();
    }
}