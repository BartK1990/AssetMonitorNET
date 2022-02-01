using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.SingletonServices
{
    public interface IAssetsSharedServiceBase<T>
    {
        List<T> AssetsData { get; set; }

        Task UpdateAssetsListBase();
    }
}
