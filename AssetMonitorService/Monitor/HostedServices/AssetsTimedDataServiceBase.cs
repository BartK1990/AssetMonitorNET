using AssetMonitorService.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.HostedServices
{
    public abstract class AssetsTimedDataServiceBase<TTimed, TIAsset, TAssetShared> : AssetsTimedServiceBase<TTimed>
    {
        protected readonly IServiceScopeFactory _scopeFactory;

        public AssetsTimedDataServiceBase(IServiceScopeFactory scopeFactory,
            ILogger<TTimed> logger, TimeSpan? scanTime = null) : base(logger, scanTime)
        {
            this._scopeFactory = scopeFactory;
        }

        protected override void TimedJob()
        {
            GetAssetsData();
        }

        protected void GetAssetsData()
        {
            bool lockTaken = false;
            try
            {
                System.Threading.Monitor.TryEnter(taskLock, ref lockTaken);
                if (lockTaken)
                {
                    using var scope = _scopeFactory.CreateScope();
                    var assetService = scope.ServiceProvider.GetRequiredService<TIAsset>();
                    var repository = scope.ServiceProvider.GetRequiredService<IAssetMonitorRepository>();

                    var assets = GetAssets().ToList();

                    if (_taskList == null)
                        return;

                    if (_taskList.Count != assets.Count)
                    {
                        _taskList = new List<Task>();
                        foreach (var a in assets)
                        {
                            _taskList.Add(GetTask(assetService, a));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < assets.Count; i++)
                        {
                            if ((_taskList[i].Status != TaskStatus.Running)
                                && (_taskList[i].Status != TaskStatus.WaitingToRun)
                                && (_taskList[i].Status != TaskStatus.WaitingForActivation)
                                )
                            {
                                _taskList[i] = GetTask(assetService, assets[i]);
                            }
                        }
                    }
                    Task.WhenAll(_taskList);
                }
                else
                {
                    _logger.LogError($"{this.GetType().Name} - Hosted Service executes too frequent");
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (lockTaken)
                {
                    System.Threading.Monitor.Exit(taskLock);
                }
            }
        }

        protected abstract IEnumerable<TAssetShared> GetAssets();
        protected abstract Task GetTask(TIAsset iAssetService, TAssetShared asset);
    }
}
