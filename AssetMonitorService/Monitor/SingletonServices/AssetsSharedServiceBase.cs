using AssetMonitorService.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.SingletonServices
{
    public abstract class AssetsSharedServiceBase<T> : IAssetsSharedServiceBase<T>
    {
        public List<T> AssetsData { get; set; }

        private readonly IServiceScopeFactory _scopeFactory;

        public AssetsSharedServiceBase(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
            UpdateAssetsListBase();
        }

        public void UpdateAssetsListBase()
        {
            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IAssetMonitorRepository>();
            UpdateAssetsList(repository);
        }

        protected abstract void UpdateAssetsList(IAssetMonitorRepository repository);
    }
}
