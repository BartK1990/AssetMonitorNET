using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.HostedServices
{
    public class AssetsTimedPerformanceDataService : IHostedService, IDisposable
    {
        private readonly ILogger<AssetsTimedPerformanceDataService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;
        public readonly TimeSpan ScanTime;
        private List<Task> _taskList = new List<Task>();
        private readonly object taskLock = new object();
        public const int AgentTcpPort = 9560;

        public AssetsTimedPerformanceDataService(ILogger<AssetsTimedPerformanceDataService> logger, IServiceScopeFactory scopeFactory,
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

        private void GetAssetsData(object state)
        {
            using var scope = _scopeFactory.CreateScope();
            var assetService = scope.ServiceProvider.GetRequiredService<IAssetGetPerformanceDataService>();
            var assetRepository = scope.ServiceProvider.GetRequiredService<IAssetMonitorRepository>();

            var assets = assetRepository.GetAllAssetsAsync().Result.ToList();

            if (_taskList == null)
                return;

            lock (taskLock)
            {
                if (_taskList.Count != assets.Count)
                {
                    _taskList = new List<Task>();
                    foreach (var a in assets)
                    {
                        _taskList.Add(assetService.GetAssetsDataAsync(a.IpAddress, AgentTcpPort));
                        _logger.LogInformation($"Service {this.GetType().Name} request to: {a.IpAddress}:{AgentTcpPort}");
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
                            _taskList[i] = assetService.GetAssetsDataAsync(assets[i].IpAddress, AgentTcpPort);
                            _logger.LogInformation($"Service {this.GetType().Name} request to: {assets[i].IpAddress}:{AgentTcpPort}");
                        }
                    }
                }
            }
            try
            {
                Task.WaitAll(_taskList.ToArray());
            }
            catch (AggregateException e)
            {
                var sb = new StringBuilder();
                sb.Append("The following exceptions have been thrown by WaitAll(): ");
                foreach (var ae in e.InnerExceptions)
                {
                    sb.Append($"|{ae.Message}");
                }
                _logger.LogWarning(sb.ToString());
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
