using Amazon.Runtime;
using Amazon.S3;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using HW.Gateway.Services;
using HW.GatewayApi.Admin;
using HW.GatewayApi.AdminService;
using HW.GatewayApi.AdminServices;
using HW.GatewayApi.AuthO;
using HW.GatewayApi.Code;
using HW.GatewayApi.Controllers;
using HW.GatewayApi.Helpers;
using HW.GatewayApi.Services;
using HW.Http;
using HW.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace HW.GatewayApi
{
    public class Startup
    {
        const string CorsPolicyName = "CorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc().AddNewtonsoftJson();
            services.AddSingleton(Configuration.GetSection("ClientCredentials").Get<ClientCredentials>());

            var appSettingsSection = Configuration.GetSection("ServiceConfiguration");
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            var awsOption = Configuration.GetAWSOptions();
            awsOption.Credentials = new BasicAWSCredentials(Configuration["AWS:AccessKey"], Configuration["AWS:SecretKey"]);
            services.AddDefaultAWSOptions(awsOption);
            services.AddAWSService<IAmazonS3>();
            services.Configure<ServiceConfiguration>(appSettingsSection);

            services.AddScoped<IAdminCustomerServices, AdminCustomerService>();
            services.AddScoped<IAdminIdentityServer, AdminIdentityServer>();
            services.AddScoped<IAdminSupplierService, AdminSupplierService>();
            services.AddScoped<IAdminTradesmanService, AdminTradesmanService>();
            services.AddScoped<IAdminUserManagmentService, AdminUserManagmentService>();
            services.AddScoped<IAdminPackagesAndPaymentsService, AdminPackagesAndPaymentsService>();
            services.AddScoped<IAdminCMSService, AdminCMSService>();
            services.AddScoped<IAdminElmahService, AdminElmahService>();
            services.AddScoped<IAdminJobServices, AdminJobServices>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ILoggingService, LoggingService>();
            services.AddScoped<ITradesmanService, TradesmanService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IAnalyticsService, AnalyticsService>();
            services.AddScoped<IPromotionService, PromotionService>();
            services.AddScoped<IPackageAndPaymentService, PackageAndPaymentService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPackageAndPaymentService, PackageAndPaymentService>();
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<ICommunicationService, CommunicationService>();
            services.AddScoped<IImagesService, ImagesService>();
            services.AddScoped<IAdminNotificationService, AdminNotificationService>();
            services.AddScoped<IVideoService, VideoService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IExceptionService, ExceptionService>();
            services.AddScoped<IAWSS3FileService, AWSS3FileService>();
            services.AddScoped<IAWSS3BucketHelper, AWSS3BucketHelper>();
            services.AddScoped<JobController>();
            services.AddCors().AddMvcCore();
            string ElmahConnectionString = Configuration.GetConnectionString("ElmahConnectionString");
            //Custom Policy.. // 
            // Means After user Authentication , On Basis of a Specific Condition We Provide the Authorization 
            // of a User. 
            // This  Specific  Conditions is called the  Policy in .net core.
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("RolePolicy", policy => policy.RequireScope("role"));

            });

            services.AddElmah<SqlErrorLog>(options =>
            {
                options.ConnectionString = ElmahConnectionString;
            });

            var apiConfig = Configuration.GetSection(typeof(ApiConfig).Name).Get<ApiConfig>();
            services.AddSingleton(apiConfig);
            services.AddAuthentication("Bearer")
                //.AddJwtBearer(options =>
                //{
                //    options.TokenValidationParameters = new TokenValidationParameters()
                //    {
                //        ClockSkew = TimeSpan.FromMinutes(2)
                //    };
                //})
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = apiConfig.IdentityServerApiUrl;
                    //options.Authority = ApiRoutes.IdentityServer.BaseUrl;
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "Gateway Api";
                    options.RoleClaimType = "role";
                    options.JwtValidationClockSkew = TimeSpan.FromMinutes(1);

                    options.JwtBearerEvents.OnMessageReceived = context =>
                    {
                        Console.WriteLine("OnMessageReceived: " + context.Token);

                        return Task.CompletedTask;
                    };

                    options.JwtBearerEvents.OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);

                        return Task.CompletedTask;
                    };

                    options.JwtBearerEvents.OnTokenValidated = context =>
                    {
                        Console.WriteLine("OnTokenValidated: " + context.SecurityToken);

                        var token = JsonConvert.DeserializeObject<TokenHeader>(context.SecurityToken.ToString().Split('.')[0]);

                        if (token.alg != ClaimValue.Algo) // constants for alog i.e RS256
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Fail($"Signed or encrypted token must have non-empty crypto segment..");
                            return Task.CompletedTask;
                        }
                        else return Task.CompletedTask;
                    };

                    options.JwtBearerEvents.OnChallenge = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.HandleResponse();

                        var errorCode = ErrorCode.Unauthorized;

                        if (context.ErrorDescription.ToLower().Contains("token is expired"))
                            errorCode = ErrorCode.TokenExpired;
                        else if (context.ErrorDescription.ToLower().Contains("audience is invalid"))
                            errorCode = ErrorCode.InvalidAudience;

                        var payload = new JObject
                        {
                            ["error"] = context.Error,
                            ["errorDescription"] = context.ErrorDescription,
                            ["errorUri"] = context.ErrorUri,
                            ["code"] = (int)errorCode
                        };

                        return context.Response.WriteAsync(payload.ToString());
                    };
                });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                  //  builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseElmah();
            app.UseRouting();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute(name: "default", pattern: "api/{controller}/{action}/{id?}");
                routes.MapControllerRoute(name: "default-start", pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
