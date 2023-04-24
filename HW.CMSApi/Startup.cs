﻿using ElmahCore.Mvc;
using ElmahCore.Sql;
using HW.CMSApi.Services;
using HW.CMSModels;
using HW.Http;
using HW.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HW.CMSApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public Autofac.IContainer ApplicationContainer { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<CMSContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
            services.AddScoped<IUnitOfWork, UnitOfWork<CMSContext>>();
            services.AddScoped<ICMSService, CMSService>();
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

                  options.ApiName = "CMSApi";
              });

            //var builder = new ContainerBuilder();
            //builder.Register(context =>
            //{
            //    var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            //    {
            //        var host = cfg.Host(new Uri("rabbitmq://rabbitmqserver/"), h =>
            //        {
            //            h.Username("devuser");
            //            h.Password("devuser");
            //        });

            //        cfg.ExchangeType = ExchangeType.Fanout;
            //        //cfg.ReceiveEndpoint(host, "queue" + Guid.NewGuid().ToString(), e =>
            //        //{
            //        //    e.LoadFrom(context);
            //        //});
            //    });

            //    return busControl;
            //})
            //    .As<IBusControl>()
            //    .As<IBus>()
            //    .As<IPublishEndpoint>()
            //    .SingleInstance();

            //builder.Populate(services);
            //ApplicationContainer = builder.Build();

            //return new AutofacServiceProvider(ApplicationContainer);
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
