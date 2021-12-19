using AssetMonitorDataAccess.DataAccess;
using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.HostedServices;
using AssetMonitorService.Monitor.Services;
using AssetMonitorService.Monitor.SingletonServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            // gRPC
            //services.AddCodeFirstGrpc();

            // Hosted services
            services.AddHostedService<AssetsTimedPingService>();
            services.AddHostedService<AssetsTimedPerformanceDataService>();

            // Shared (Singleton) services
            services.AddSingleton<IAssetsPingSharedService, AssetsPingSharedService>();
            services.AddSingleton<IAssetsPerformanceDataSharedService, AssetsPerformanceDataSharedService>();

            // Scoped services
            services.AddScoped<IAssetPingService, AssetPingService>();
            services.AddScoped<IAssetGetPerformanceDataService, AssetGetPerformanceDataService>();

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
                //endpoints.MapGrpcService<GreeterService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
