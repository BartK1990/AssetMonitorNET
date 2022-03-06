using AssetMonitorDataAccess.DataAccess;
using AssetMonitorHistoryDataAccess.DataAccess;
using AssetMonitorService.Data.Repositories;
using AssetMonitorService.gRPC.CommunicationServices;
using AssetMonitorService.Monitor.HostedServices;
using AssetMonitorService.Monitor.Services;
using AssetMonitorService.Monitor.Services.Asset.Live;
using AssetMonitorService.Monitor.Services.Email;
using AssetMonitorService.Monitor.SingletonServices;
using AssetMonitorService.Monitor.SingletonServices.Alarm;
using AssetMonitorService.Monitor.SingletonServices.Email;
using AssetMonitorService.Monitor.SingletonServices.Historical;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProtoBuf.Grpc.Server;

namespace AssetMonitorService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // EF Core
            services.AddDbContext<AssetMonitorContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:AssetMonitorContextDb"]);
            });
            services.AddScoped<IAssetMonitorRepository, AssetMonitorRepository>();
            services.AddDbContext<AssetMonitorHistoryContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:AssetMonitorHistoryContextDb"]);
            });
            services.AddScoped<IAssetMonitorHistoryRepository, AssetMonitorHistoryRepository>();

            // Singleton for Micro ORM Dapper
            services.AddSingleton<AssetMonitorHistoryDapperContext>(new AssetMonitorHistoryDapperContext(Configuration["ConnectionStrings:AssetMonitorHistoryContextDb"]));
            services.AddScoped<IAssetMonitorHistoryDapperRepository, AssetMonitorHistoryDapperRepository>();

            // gRPC
            services.AddCodeFirstGrpc();

            // Hosted services
            services.AddHostedService<InitSharedServices>();
            services.AddHostedService<AssetsTimedIcmpDataService>();
            services.AddHostedService<AssetsTimedPerformanceDataService>();
            services.AddHostedService<AssetsTimedSnmpDataService>();
            services.AddHostedService<AssetsHistoryTimedService>();
            services.AddHostedService<AssetsAlarmTimedService>();
            services.AddHostedService<AssetsNotificationTimedService>();

            // Shared (Singleton) services
            services.AddSingleton<IApplicationPropertiesService, ApplicationPropertiesService>();
            // Shared (Singleton) services for Assets Live Data
            services.AddSingleton<IAssetsCollectionSharedService, AssetsCollectionSharedService>();
            services.AddSingleton<IAssetsIcmpSharedService, AssetsIcmpDataSharedService>();
            services.AddSingleton<IAssetsPerformanceDataSharedService, AssetsPerformanceDataSharedService>();
            services.AddSingleton<IAssetsSnmpDataSharedService, AssetsSnmpDataSharedService>();
            // Shared (Singleton) services for Historical Data
            services.AddSingleton<IHistoricalTablesSharedService, HistoricalTablesSharedService>();
            services.AddSingleton<IAssetsHistoricalDataSharedService, AssetsHistoricalDataSharedService>();
            // Shared (Singleton) services for Alarm Data
            services.AddSingleton<IAssetsAlarmDataSharedService, AssetsAlarmDataSharedService>();
            // Shared (Singleton) services for Emails
            services.AddSingleton<IAssetsNotificationDataSharedService, AssetsNotificationDataSharedService>();
            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());

            // Scoped services
            services.AddScoped<IAssetIcmpDataService, AssetIcmpDataService>();
            services.AddScoped<IAssetPerformanceDataService, AssetPerformanceDataService>();
            services.AddScoped<IAssetSnmpDataService, AssetSnmpDataService>();

            // Transient services
            services.AddTransient<IEmailService, EmailService>();
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
                endpoints.MapGrpcService<AssetMonitorDataService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
