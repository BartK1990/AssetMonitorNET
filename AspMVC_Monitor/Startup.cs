using AspMVC_Monitor.Data.Repositories;
using AspMVC_Monitor.Models;
using AspMVC_Monitor.Services.HostedServices;
using AspMVC_Monitor.Services.ScopedServices;
using AspMVC_Monitor.Services.SingletonServices;
using AssetMonitorDataAccess.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddScoped<IAssetMonitorRepository, AssetMonitorRepository>();

            services.AddControllersWithViews();
            services.AddSession();

            // Hosted services
            services.AddHostedService<AssetsDataTimedService>();

            // Singletion services
            services.AddSingleton<IApplicationPropertiesService, ApplicationPropertiesService>();
            services.AddSingleton<IAssetsLiveDataShared, AssetsLiveDataShared>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env)
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
            });

        }
    }
}
