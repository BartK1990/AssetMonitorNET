using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorSharedGRPC.Agent;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WindowsDataLib;

namespace AssetMonitorAgent.SingletonServices
{
    public class AssetDataSharedService : IAssetDataSharedService
    {
        private readonly object _dataLock = new object();

        private readonly ILogger<AssetDataSharedService> _logger;

        public IDictionary<AssetDataItemRequest, object> Data { get; set; } = 
            new Dictionary<AssetDataItemRequest, object>(new AssetDataItemRequestEqualityComparer());

        public AssetDataSharedService(ILogger<AssetDataSharedService> logger)
        {
            this._logger = logger;
        }

        public void UpdateConfiguration(IEnumerable<AssetDataItemRequest> dataItems)
        {
            lock (_dataLock)
            {
                bool configurationChanged = false;
                foreach (var d in dataItems)
                {
                    if (!Data.ContainsKey(d))
                    {
                        configurationChanged = true;
                        Data.Add(d, new object());
                    }
                }
                if (configurationChanged)
                {
                    _logger.LogInformation($"Tag set configuration changed. Updating values...");
                    UpdateData();
                }
            }
        }

        public void UpdateData()
        {
            lock (_dataLock)
            {
                var keys = new List<AssetDataItemRequest>();
                keys.AddRange(Data.Keys);
                foreach (var key in keys)
                {
                    if (Data.TryGetValue(key, out _))
                    {
                        switch (key.AgentDataTypeId)
                        {
                            case (int)AgentDataTypeEnum.PerformanceCounter:
                                Data[key] = WindowsPerformance.GetPerformanceCounterValue(key.PerformanceCounter);
                                break;
                            case (int)AgentDataTypeEnum.WMI:
                                Data[key] = WindowsPerformance.GetWmiValue(key.WmiManagementObject);
                                break;
                            case (int)AgentDataTypeEnum.ServiceState:
                                Data[key] = WindowsService.GetServiceState(key.ServiceName);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
