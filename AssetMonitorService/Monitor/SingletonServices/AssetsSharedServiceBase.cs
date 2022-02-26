using AssetMonitorDataAccess.Models;
using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorService.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.SingletonServices
{
    public abstract class AssetsSharedServiceBase<TShared,T> : IAssetsSharedServiceBase<T>
    {
        public List<T> AssetsData { get; set; }

        protected readonly IServiceScopeFactory _scopeFactory;
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

        protected TParam GetAssetProperty<TParam>(Asset asset, ICollection<AssetPropertyValue> assetProperties, AssetPropertyNameEnum assetPropertyName, Func<string, TParam> parse, TParam defaultValue)
        {
            TParam param = defaultValue;
            var errorMessage = $"Wrong {AssetPropertyNameDictionary.Dict[assetPropertyName]} for Asset: {asset.Name} (Id: {asset.Id}) | Default [{param}] used";
            var result = assetProperties?
                .FirstOrDefault(p => p.AssetPropertyId == (int)assetPropertyName)?.Value ?? null;
            if (result != null)
            {
                try
                {
                    param = parse(result);
                }
                catch
                {
                    _logger.LogError(errorMessage);
                }
            }
            else
            {
                _logger.LogError(errorMessage);
            }

            return param;
        }

        protected string FuncString(string s)
        {
            return s;
        }
    }
}
