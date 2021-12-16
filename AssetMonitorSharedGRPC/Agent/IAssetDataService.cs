using ProtoBuf.Grpc;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AssetMonitorSharedGRPC.Agent
{
    [ServiceContract]
    public interface IAssetDataService
    {
        [OperationContract]
        Task<AssetDataReply> GetAssetDataAsync(AssetDataRequest request, CallContext context = default);
    }
}
