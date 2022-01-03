using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using System;
using System.Net.Http;

namespace AssetMonitorService.gRPC
{
    public static class GrpcHelper<T>
        where T : class
    {
        public static T CreateSecureClient(string hostname, int tcpPort)
        {
            // Restrict secure (HTTPS) connection
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", false);

            // Allow untrusted certificates
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            GrpcChannel channel = GrpcChannel.ForAddress($"https://{hostname}:{tcpPort}",
                            new GrpcChannelOptions { HttpHandler = httpHandler });
            return channel.CreateGrpcService<T>();
        }

        public static T CreateUnsecureClient(string hostname, int tcpPort)
        {
            // Allow unsecure (HTTP) connection
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            // Allow untrusted certificates
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            GrpcChannel channel = GrpcChannel.ForAddress($"http://{hostname}:{tcpPort}",
                            new GrpcChannelOptions { HttpHandler = httpHandler });
            return channel.CreateGrpcService<T>();
        }
    }
}
