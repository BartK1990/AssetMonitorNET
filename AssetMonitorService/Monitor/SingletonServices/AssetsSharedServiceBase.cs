using AssetMonitorService.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.SingletonServices
{
    public abstract class AssetsSharedServiceBase<TShared,T> : IAssetsSharedServiceBase<T>
    {
        public List<T> AssetsData { get; set; }

        private readonly IServiceScopeFactory _scopeFactory;
        protected readonly ILogger<TShared> _logger;

        public AssetsSharedServiceBase(IServiceScopeFactory scopeFactory,
            ILogger<TShared> logger)
        {
            this._scopeFactory = scopeFactory;
            this._logger = logger;
            this.AssetsData = new List<T>();
        }

        public async Task UpdateAssetsListBase()
        {
            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IAssetMonitorRepository>();
            await UpdateAssetsList(repository);
            _logger.LogInformation($"{this.GetType().Name} - Shared Service initialized");
        }

        protected abstract Task UpdateAssetsList(IAssetMonitorRepository repository);
    }
}
