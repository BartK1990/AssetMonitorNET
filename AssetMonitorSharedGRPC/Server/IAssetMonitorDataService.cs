using ProtoBuf.Grpc;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AssetMonitorSharedGRPC.Server
{
    [ServiceContract]
    public interface IAssetMonitorDataService
    {
        [OperationContract]
        Task<AssetSnmpUpdateCommandReply> UpdateAssetSnmpValuesById(AssetSnmpUpdateCommandRequest request, CallContext context = default);
        [OperationContract]
        Task<AssetsDataReply> GetAssetsData(AssetsDataRequest request, CallContext context = default);
        [OperationContract]
        Task<AssetsDataConfigurationReply> GetAssetsDataConfiguration(AssetsDataConfigurationRequest request, CallContext context = default);
    }
}
