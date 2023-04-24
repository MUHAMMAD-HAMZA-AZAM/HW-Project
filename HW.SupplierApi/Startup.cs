using Autofac;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using HW.Http;
using HW.SupplierApi.Services;
using HW.SupplierModels;
using HW.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using HW.SupplierApi.Helpers;
using Microsoft.Extensions.Options;

namespace HW.SupplierApi
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
            services.Configure<ShippingApiSettings>(Configuration.GetSection("ShippingApiSettings"));
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddDbContext<SupplierContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
            services.AddScoped<IUnitOfWork, UnitOfWork<SupplierContext>>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IShippingApiService,ShippingApiService>();
            services.AddScoped<IHttpClientService, HttpClientService>(); // 
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IExceptionService, ExceptionService>();
            string ElmahConnectionString = Configuration.GetConnectionString("ElmahConnectionString");

            services.AddElmah<SqlErrorLog>(options =>
            {
                options.ConnectionString = ElmahConnectionString;
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
