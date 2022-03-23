using AspMVC_Monitor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Services.SingletonServices
{
    public interface IAssetsLiveDataShared
    {
        public List<AssetLiveData> AssetsData { get; }
        public Task UpdateAssetsLiveData();
    }
}
