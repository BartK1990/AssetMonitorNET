using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace AssetMonitorAgent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseWindowsService()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.ConfigureKestrel(options =>
                    //{
                    //    options.Listen(IPAddress.Any, 9560, listenOptions =>
                    //    {
                    //        listenOptions.Protocols = HttpProtocols.Http2;
                    //        listenOptions.UseHttps("<path to .pfx file>",
                    //            "<certificate password>");
                    //    });
                    //});
                    webBuilder.UseStartup<Startup>();
                });
    }
}
