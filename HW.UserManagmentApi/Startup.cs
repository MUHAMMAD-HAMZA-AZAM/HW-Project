using ElmahCore.Mvc;
using ElmahCore.Sql;
using HW.Http;
using HW.UserManagementApi.Services;
using HW.UserManagmentModels;
using HW.Utility;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HW.UserManagmentApi
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
            services.AddControllers();
            //services.AddDbContext<UserManagementContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
            //
            var conn = string.Empty;
            services.AddDbContext<UserManagementContext>((serviceProvider, options) =>
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnectionString");
                conn = connectionString;
                if (!string.IsNullOrWhiteSpace(connectionString))
                    options.UseSqlServer(connectionString);

            });
            string ElmahConnectionString = Configuration.GetConnectionString("ElmahConnectionString");

            services.AddElmah<SqlErrorLog>(options =>
            {
                options.ConnectionString = ElmahConnectionString;
            });
            services.AddScoped<IDbConnection>((sp) => new SqlConnection(conn));
            //

            services.AddScoped<IUnitOfWork, UnitOfWork<UserManagementContext>>();
            services.AddScoped<IUserManagementService, UserManagementService>();

            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IExceptionService, ExceptionService>();
            

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
