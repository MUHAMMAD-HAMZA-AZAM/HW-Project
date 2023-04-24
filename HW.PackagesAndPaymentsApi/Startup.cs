using Autofac;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using HW.Http;
using HW.PackagesAndPaymentsApi.Services;
using HW.PackagesAndPaymentsModels;
using HW.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HW.PackagesAndPaymentsApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IContainer ApplicationContainer { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //  services.AddDbContext<PackagesAndPaymentsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
            var conn = string.Empty;
            services.AddDbContext<PackagesAndPaymentsContext>((serviceProvider, options) =>
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnectionString");
                conn = connectionString;
                if (!string.IsNullOrWhiteSpace(connectionString))
                    options.UseSqlServer(connectionString);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork<PackagesAndPaymentsContext>>();
            services.AddScoped<IPackagesAndPaymentsService, PackagesAndPaymentsService>();
            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddScoped<IExceptionService, ExceptionService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IDbConnection>((sp) => new SqlConnection(conn));

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

                  options.ApiName = "PackagesAndPaymentsApi";
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

            //var bus = ApplicationContainer.Resolve<IBusControl>();
            //var busHandle = TaskUtil.Await(() => bus.StartAsync());
            //lifetime.ApplicationStopping.Register(() => busHandle.Stop());
        }
    }
}
