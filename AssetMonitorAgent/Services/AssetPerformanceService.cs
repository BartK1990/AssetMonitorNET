using AssetMonitorAgent.SingletonServices;
using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorSharedGRPC.Agent;
using System.Collections.Generic;
using WindowsDataLib;

namespace AssetMonitorAgent.Services
{
    public class AssetPerformanceService : IAssetPerformanceService
    {
        public IList<object> GetData(AssetDataRequest request)
        {
            var dataList = new List<object>();
            foreach (var r in request.Tags)
            {
                switch (r.AgentDataTypeId)
                {
                    case (int)AgentDataTypeEnum.PerformanceCounter:
                        dataList.Add(WindowsPerformance.GetPerformanceCounterValue(r.PerformanceCounter));
                        break;
                    case (int)AgentDataTypeEnum.WMI:
                        dataList.Add(WindowsPerformance.GetWmiValue(r.WmiManagementObject));
                        break;
                    case (int)AgentDataTypeEnum.ServiceState:
                        dataList.Add(WindowsService.GetServiceState(r.ServiceName));
                        break;
                    default:
                        break;
                }
            }
            return dataList;
        }

        public IList<object> GetData(IAssetDataSharedService assetDataSharedService)
        {
            throw new System.NotImplementedException();
        }
    }
}
