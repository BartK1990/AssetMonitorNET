using AssetMonitorSharedGRPC;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using System;

namespace ConsolegRPC
{
    class Program
    {
        static void Main(string[] args)
        {
            // The port number must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:9561");
            Console.WriteLine("Press any key to send a message");
            Console.ReadKey();
            var name = "Console gRPC";
            Console.WriteLine($"{name} saying Hello");

            var client = channel.CreateGrpcService<IGreeterService>();
            var reply = client.SayHelloAsync(
                new HelloRequest { Name = name }).Result;

            foreach (var m in reply.Message)
            {
                Console.WriteLine("Greeting: " + m);
            }
            //Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
