using AssetMonitorService.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.HostedServices
{
    public abstract class AssetsTimedServiceBase<TTimed, TIAsset, TAssetShared> : IHostedService, IDisposable
    {
        public readonly TimeSpan ScanTime;
        protected readonly ILogger<TTimed> _logger;
        protected readonly IServiceScopeFactory _scopeFactory;
        protected readonly object taskLock = new object();
        protected Timer _timer;
        protected List<Task> _taskList = new List<Task>();

        public AssetsTimedServiceBase(ILogger<TTimed> logger, 
            IServiceScopeFactory scopeFactory,
            TimeSpan? scanTime = null)
        {
            if (scanTime != null)
            {
                this.ScanTime = (TimeSpan)scanTime;
            }
            else
            { // Default value
                this.ScanTime = TimeSpan.FromSeconds(10);
            }
            this._logger = logger;
            this._scopeFactory = scopeFactory;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{this.GetType().Name} - Hosted Service is running.");

            _timer = new Timer(
                GetAssetsData,
                null,
                TimeSpan.Zero,
                ScanTime
            );

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{this.GetType().Name} - Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        protected void GetAssetsData(object state)
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
            catch (Exception e)
            {
                _logger.LogError($"{this.GetType().Name} - Hosted Service exception!");
                _logger.LogDebug(e.Message);
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