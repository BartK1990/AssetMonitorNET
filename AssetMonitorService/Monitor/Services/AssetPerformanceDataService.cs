using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorService.gRPC;
using AssetMonitorService.Monitor.Model.Live;
using AssetMonitorSharedGRPC.Agent;
using AssetMonitorSharedGRPC.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WindowsDataLib;

namespace AssetMonitorService.Monitor.Services
{
    public class AssetPerformanceDataService : IAssetPerformanceDataService
    {
        private readonly ILogger<AssetPerformanceDataService> _logger;

        public AssetPerformanceDataService(ILogger<AssetPerformanceDataService> logger)
        {
            this._logger = logger;
        }

        public async Task UpdateAsset(AssetPerformanceData assetPerformanceData)
        {
           await UpdateAssetWithScanTime(assetPerformanceData);
        }

        public async Task UpdateAsset(AssetPerformanceData assetPerformanceData, int scanTime)
        {
            await UpdateAssetWithScanTime(assetPerformanceData, scanTime);
        }

        // Default value of scanTime is 10 second if not specified
        private async Task UpdateAssetWithScanTime(AssetPerformanceData assetPerformanceData, int scanTime = 10)
        {
            try
            {
                // Get from local windows machine
                if (assetPerformanceData.IpAddress == IPAddress.Loopback.ToString())
                {
                    foreach (var d in assetPerformanceData.Data)
                    {
                        switch (d.Key.AgentDataType)
                        {
                            case AgentDataTypeEnum.PerformanceCounter:
                                d.Value.Value = WindowsPerformance.GetPerformanceCounterValue(d.Key.PerformanceCounter);
                                break;
                            case AgentDataTypeEnum.WMI:
                                d.Value.Value = WindowsPerformance.GetWmiValue(d.Key.WmiManagementObject);
                                break;
                            case AgentDataTypeEnum.ServiceState:
                                d.Value.Value = WindowsService.GetServiceState(d.Key.ServiceName);
                                break;
                            default:
                                break;
                        }
                    }
                    return;
                }

                // Get from gRPC server
                var reply = await GetAssetDataAsync(assetPerformanceData, scanTime);
                var replyDataList = reply.Data.ToList();

                if (replyDataList.Count <= 0 || (replyDataList.Count != assetPerformanceData.Data.Count))
                    return;

                for (int i = 0; i < replyDataList.Count; i++)
                {
                    assetPerformanceData.Data.ElementAt(i).Value.Value = ByteConverterHelper.ByteArrayToObject(replyDataList[i].ByteArray);
                }
            }
            catch // put null values if request failed
            {
                foreach (var d in assetPerformanceData.Data)
                {
                    d.Value.Value = null;
                }
                throw;
            }
        }

            private async Task<AssetDataReply> GetAssetDataAsync(AssetPerformanceData assetPerformanceData, int scanTime)
        {          
            if (assetPerformanceData.TcpPort == null)
            {
                throw new ArgumentNullException("No TCP port specified");
            }
            int port = (int)assetPerformanceData.TcpPort;

            var requestTags = new List<AssetDataItemRequest>();
            foreach (var d in assetPerformanceData.Data)
            {
                requestTags.Add(new AssetDataItemRequest() 
                {
                    DataType = (int)d.Key.ValueDataType,
                    AgentDataTypeId = (int)d.Key.AgentDataType, 
                    PerformanceCounter = d.Key.PerformanceCounter,
                    WmiManagementObject = d.Key.WmiManagementObject,
                    ServiceName = d.Key.ServiceName
                });
            }
            AssetDataReply reply = new AssetDataReply();
            try
            {
                var client = GrpcHelper<IAssetDataService>.CreateUnsecureClient(assetPerformanceData.IpAddress, port);
                reply = await client.GetAssetDataAsync( new AssetDataRequest(scanTime: scanTime, tags: requestTags));
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Cannot retrieve data from Agent: {assetPerformanceData.IpAddress}:{port}");
                _logger.LogDebug($"Exception: { ex.Message}");
            }
            return reply;
        }

    }
}
