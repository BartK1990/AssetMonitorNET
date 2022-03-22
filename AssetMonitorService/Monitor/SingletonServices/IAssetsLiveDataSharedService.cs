using AssetMonitorService.Monitor.Model.Live;

namespace AssetMonitorService.Monitor.SingletonServices
{
    public interface IAssetsLiveDataSharedService : IAssetsSharedServiceBase<AssetLiveData>
    {
        public bool AssetsDataNewConfiguration { get; }
        public void AssetsDataNewConfigurationClear();
    }
}
