using Autofac;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using HW.Http;
using HW.LoggingApi.DbLogProvider;
using HW.LoggingApi.Messaging.Consumers;
using HW.LoggingApi.Services;
using HW.LoggingModels;
using HW.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HW.LoggingApi
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

            services.AddDbContext<LoggingContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
            services.AddScoped<IUnitOfWork, UnitOfWork<LoggingContext>>();
            services.AddScoped<ILoggingService, LoggingService>();
            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddScoped<IExceptionService, ExceptionService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            string ElmahConnectionString = Configuration.GetConnectionString("ElmahConnectionString");

            services.AddElmah<SqlErrorLog>(options =>
            {
                options.ConnectionString = ElmahConnectionString;
            });

            var builder = new ContainerBuilder();

            // register a specific consumer
            builder.RegisterType<LogEventConsumer>();

            //builder.Register(context =>
            //{
            //    var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            //    {
            //        var host = cfg.Host(new Uri("rabbitmq://rabbitmqserver/"), h =>
            //        {
            //            h.Username("devuser");
            //            h.Password("devuser");
            //        });

            //        cfg.ReceiveEndpoint(host, "queue" + Guid.NewGuid().ToString(), e =>
            //        {
            //            e.LoadFrom(context);
            //        });
            //    });

            //    return busControl;
            //}).SingleInstance().As<IBusControl>().As<IBus>();

            //builder.Populate(services);
            //ApplicationContainer = builder.Build();
            //return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddContext(LogLevel.Information, Configuration.GetConnectionString("ConnectionString"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseElmah();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "api/{controller}/{action}/{id?}");
            });
        }
    }
}
