using AssetMonitorSharedGRPC.Agent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AssetMonitorAgent.SingletonServices
{
    public class AssetDataItemRequestEqualityComparer : IEqualityComparer<AssetDataItemRequest>
    {
        public bool Equals([AllowNull] AssetDataItemRequest x, [AllowNull] AssetDataItemRequest y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            if (x.AgentDataTypeId == y.AgentDataTypeId &&
                x.PerformanceCounter == y.PerformanceCounter &&
                x.WmiManagementObject == y.WmiManagementObject &&
                x.ServiceName == y.ServiceName)
                return true;

            return false;
        }

        public int GetHashCode([DisallowNull] AssetDataItemRequest obj)
        {
            var sum = obj.AgentDataTypeId.GetHashCode();
            if (obj.PerformanceCounter != null)
                sum += obj.PerformanceCounter.GetHashCode()*23;
            if (obj.PerformanceCounter != null)
                sum += obj.PerformanceCounter.GetHashCode()*123;
            if (obj.PerformanceCounter != null)
                sum += obj.PerformanceCounter.GetHashCode()*3452;
            return sum;
        }
    }
}
