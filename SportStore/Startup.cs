using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SportStore.Models;
using SportStore.Models.Repo;

namespace SportStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSession();

            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
                options.UseLoggerFactory(LoggerFactory.Create(configure => configure.AddConsole()));
            });

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<CartBase>(SessionCart.GetCart);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseSession();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", "{controller=Store}/{action=Index}/{id?}");
            });
        }
    }
}
