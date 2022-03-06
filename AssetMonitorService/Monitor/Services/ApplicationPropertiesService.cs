using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorService.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services
{
    public class ApplicationPropertiesService : IApplicationPropertiesService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ApplicationPropertiesService> _logger;

        public ApplicationPropertiesService(IServiceScopeFactory scopeFactory, ILogger<ApplicationPropertiesService> logger)
        {
            this._scopeFactory = scopeFactory;
            this._logger = logger;
        }

        public async Task<TParam> GetProperty<TParam>(ApplicationPropertyNameEnum appPropertyName, Func<string, TParam> parse, TParam defaultValue)
        {
            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IAssetMonitorRepository>();

            var appProperties = await repository.GetAppPropertiesAsync();

            TParam param = defaultValue;
            var errorMessage = $"Wrong Application Property [{ApplicationPropertyNameDictionary.Dict[appPropertyName].Description}] | Default [{param}] used";
            var result = appProperties?
                .FirstOrDefault(p => p.Id == (int)appPropertyName)?.ApplicationPropertyValue?.Value ?? null;
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

        public string FuncString(string s)
        {
            return s;
        }
    }
}
