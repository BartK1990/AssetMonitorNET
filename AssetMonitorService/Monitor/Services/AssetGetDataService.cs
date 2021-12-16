using AssetMonitorSharedGRPC.Agent;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services
{
    public class AssetGetDataService : IAssetGetDataService
    {
        public int TcpPort { get; private set; }
        public IPAddress IpAddress { get; private set; }

        public AssetGetDataService(IPAddress ipAddress, int tcpPort)
        {
            this.TcpPort = tcpPort;
            this.IpAddress = ipAddress;
        }

        public async Task<string> GetAssetsDataAsync()
        {
            // Allow untrusted certificates
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            using var channel = GrpcChannel.ForAddress($"https://{IpAddress}:{TcpPort}",
                new GrpcChannelOptions { HttpHandler = httpHandler });
            var client = channel.CreateGrpcService<IAssetDataService>();
            var reply = await client.GetAssetDataAsync(
                new AssetDataRequest { Init = 1 });
            return reply.ToString();
        }
    }
}
