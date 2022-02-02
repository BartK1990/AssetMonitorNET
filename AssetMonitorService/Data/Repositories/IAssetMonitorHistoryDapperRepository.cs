using System.Threading.Tasks;

namespace AssetMonitorService.Data.Repositories
{
    public interface IAssetMonitorHistoryDapperRepository
    {
        public Task<string> GetDbVersion();
    }
}
