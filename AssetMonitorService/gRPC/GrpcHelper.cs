using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using System.Net.Http;

namespace AssetMonitorService.gRPC
{
    public static class GrpcHelper<T>
        where T : class
    {
        public static T CreateClient(string hostname, int tcpPort)
        {
            // Allow untrusted certificates
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            GrpcChannel channel = GrpcChannel.ForAddress($"https://{hostname}:{tcpPort}",
                            new GrpcChannelOptions { HttpHandler = httpHandler });
            return channel.CreateGrpcService<T>();
        }
    }
}
