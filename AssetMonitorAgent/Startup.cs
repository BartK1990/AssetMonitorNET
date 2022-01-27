﻿using AssetMonitorAgent.BackgroundServices;
using AssetMonitorAgent.CommunicationServices;
using AssetMonitorAgent.Services;
using AssetMonitorAgent.SingletonServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc.Server;
using System;

namespace AssetMonitorAgent
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCodeFirstGrpc();

            services.AddHostedService(a => new AssetTimedService(
                logger: a.GetService<ILogger<AssetTimedService>>(),
                scopeFactory: a.GetService<IServiceScopeFactory>(),
                assetDataSharedService: a.GetService<IAssetDataSharedService>(),
                scanTime: TimeSpan.FromSeconds(10)));

            services.AddSingleton<IAssetDataSharedService, AssetDataSharedService>();

            //services.AddScoped<IAssetPerformanceService, AssetPerformanceService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<AssetDataService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
