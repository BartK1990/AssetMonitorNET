using ProtoBuf.Grpc;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AssetMonitorSharedGRPC.Server
{
    [ServiceContract]
    public interface IAssetMonitorDataService
    {
        [OperationContract]
        Task<AssetsPingDataReply> GetAssetsPingData(AssetsPingDataRequest request, CallContext context = default);
        Task<AssetsPerformanceDataReply> GetAssetsPerformanceData(AssetsPerformanceDataRequest request, CallContext context = default);
    }
}
