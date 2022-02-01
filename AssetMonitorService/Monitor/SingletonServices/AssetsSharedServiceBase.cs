using AssetMonitorService.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.SingletonServices
{
    public abstract class AssetsSharedServiceBase<T> : IAssetsSharedServiceBase<T>
    {
        public List<T> AssetsData { get; set; }

        private readonly IServiceScopeFactory _scopeFactory;

        public AssetsSharedServiceBase(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
            this.AssetsData = new List<T>();
        }

        public async Task UpdateAssetsListBase()
        {
            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IAssetMonitorRepository>();
            await UpdateAssetsList(repository);
        }

        protected abstract Task UpdateAssetsList(IAssetMonitorRepository repository);
    }
}
