using AspMVC_Monitor.Data.Repositories;
using AspMVC_Monitor.Models;
using AspMVC_Monitor.Services;
using AssetMonitorDataAccess.DataAccess;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AspMVC_Monitor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // EF Core
            services.AddDbContext<AssetMonitorContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:AssetMonitorContextDb"]);
            });

            services.AddControllersWithViews();
            services.AddSession();

            services.AddSingleton<IAssetHolder, AssetHolder>();

            services.AddScoped<IAssetMonitorRepository, AssetMonitorRepository>();

            // Hangfire
            services.AddHangfire((serviceProvider, config) =>
            {
                config.UseActivator(new ServiceProviderActivator(serviceProvider));
                config.UseMemoryStorage();
            });
            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env,
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
                endpoints.MapControllerRoute(
                    name: "assets",
                    pattern: "{controller=Assets}/{action=Index}/{id?}",
                    defaults: new { controller = "Assets", action = "Index" });
                endpoints.MapHangfireDashboard();
            });

            app.UseHangfireDashboard();
            HangfireJobs(backgroundJobClient, recurringJobManager);
        }

        private void HangfireJobs(IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
        {
            backgroundJobClient.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));

            // Ping assets every 5 seconds (CRON notation)
            recurringJobManager.AddOrUpdate<IAssetHolder>("Ping Assets",
                ah => ah.UpdateAssetPingAsync(),
                "*/5 * * * * *");

            recurringJobManager.AddOrUpdate<IAssetHolder>("Get performance data",
                ah => ah.UpdateAssetPerformanceAsync(),
                "*/10 * * * * *");

        }
    }
}
