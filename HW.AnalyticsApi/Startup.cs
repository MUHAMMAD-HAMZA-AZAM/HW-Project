using ElmahCore.Mvc;
using ElmahCore.Sql;
using HW.AnalyticsApi.Services;
using HW.AnalyticsModels;
using HW.Http;
using HW.Utility;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HW.AnalyticsApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<AnalyticsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));

            services.AddScoped<IUnitOfWork, UnitOfWork<AnalyticsContext>>();
            services.AddScoped<IAnalyticsService, AnalyticsService>();
            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddScoped<IExceptionService, ExceptionService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            string ElmahConnectionString = Configuration.GetConnectionString("ElmahConnectionString");

            services.AddElmah<SqlErrorLog>(options =>
            {
                options.ConnectionString = ElmahConnectionString;
            });

            services.AddAuthentication("Bearer")
              .AddIdentityServerAuthentication(options =>
              {
                  options.Authority = ApiRoutes.IdentityServer.BaseUrl;
                  options.RequireHttpsMetadata = false;
                  options.ApiName = "AnalyticsApi";
              });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseElmah();
            app.UseAuthentication();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action}/{id?}");
            });
        }
    }
}
