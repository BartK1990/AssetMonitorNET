using AspMVC_Monitor.Models;
using AspMVC_Monitor.Services;
using Hangfire;
using Hangfire.AspNetCore;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            services.AddControllersWithViews();
            services.AddSession();

            services.AddSingleton<IAssetHolder, AssetHolder>();

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
                endpoints.MapHangfireDashboard();
            });

            app.UseHangfireDashboard();
            HangfireJobs(backgroundJobClient, recurringJobManager);
        }

        private void HangfireJobs(IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
        {
            backgroundJobClient.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
            recurringJobManager.AddOrUpdate("Rune every minute",
                () => Console.WriteLine("Test recurring job"),
                "*/5 * * * * *");
            recurringJobManager.AddOrUpdate<IAssetHolder>("Ping Assets",
                ah => ah.UpdateAssetPingAsync(),
                "*/5 * * * * *");

        }
    }
}
