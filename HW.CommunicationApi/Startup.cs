using HW.CommunicationApi.Code;
using HW.CommunicationApi.Services;
using HW.Http;
using HW.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZongApi;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Microsoft.AspNetCore.Http;
using HW.CommunicationModels;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using Hangfire.SqlServer;

namespace HW.CommunicationApi
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
            services.AddSingleton(Configuration.GetSection("GenericAppConfig").Get<GenericAppConfig>());
            services.AddSingleton(Configuration.GetSection("UfoneSmsApiConfig").Get<UfoneSmsApiConfig>());
            services.AddSingleton(Configuration.GetSection("ZongSmsApiConfig").Get<ZongSmsApiConfig>());
            services.AddSingleton(Configuration.GetSection("JazzSmsApiConfig").Get<JazzSmsApiConfig>());
            services.AddSingleton(Configuration.GetSection("SmtpConfig:AwsSmtpConfig").Get<AwsSmtpConfig>());
            services.AddSingleton(Configuration.GetSection("SmtpConfig:GmailSmtpConfig").Get<SmtpConfig>());
            //services.AddSingleton(Configuration.GetSection("SmtpConfig:PleskSmtpConfig").Get<SmtpConfig>());
            var apiConfig = Configuration.GetSection(typeof(ApiConfig).Name).Get<ApiConfig>();
            services.AddSingleton(apiConfig);
            services.AddDbContext<CommunicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICorporateCBS, CorporateCBSClient>();     //Zong service
            services.AddScoped<ISmsService, SmsService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUnitOfWork, UnitOfWork<CommunicationContext>>();
            services.AddScoped<IViewRenderService, ViewRenderService>();

            services.AddScoped<IExceptionService, ExceptionService>();

            #region Hangfire Configration
            services.AddHangfire(configuration => configuration
            .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnectionString"), new SqlServerStorageOptions
            {

            }));

            services.AddHangfireServer();

            #endregion

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

            app.UseHangfireDashboard();
            app.UseHangfireServer();
            app.UseElmah();
            app.UseAuthentication();
            app.UseRouting();

            //Reference from https://stackoverflow.com/a/35219562/7122823
            RecurringJob.AddOrUpdate<IEmailService>(EmailService => EmailService.SendScheduledEmail(), "*/10 * * * *");
            RecurringJob.AddOrUpdate<IEmailService>(EmailService => EmailService.SendScheduledOtpEmail(), "*/5 * * * *");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action}/{id?}");
            });
        }
    }
}
