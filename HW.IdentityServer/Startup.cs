using ElmahCore.Mvc;
using ElmahCore.Sql;
using HW.Http;
using HW.IdentityServer.Data;
using HW.IdentityServer.Models;
using HW.IdentityServer.Models.Facebook;
using HW.IdentityServer.Services;
using HW.IdentityServerModels;
using HW.Utility;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace HW.IdentityServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        private readonly ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment, ILogger<Startup> logger)
        {
            Configuration = configuration;
            Environment = environment;
            _logger = logger;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
         //   string connectionString = "Data Source = 110.39.179.18,15751; Initial Catalog = HWIdentity; User ID = sa; Password = @Syst123; TrustServerCertificate = True; Encrypt = false";

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<HWIdentityContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<AspNetUserManager<ApplicationUser>, AspNetUserManager<ApplicationUser>>();

            services.AddIdentity<ApplicationUser, IdentityRole>(x =>
            {
                x.Password.RequiredLength = PasswordRules.RequiredLength;
                x.Password.RequireLowercase = PasswordRules.RequireLowercase;
                x.Password.RequireUppercase = PasswordRules.RequireUppercase;
                x.Password.RequireDigit = PasswordRules.RequireDigit;
                x.Password.RequireNonAlphanumeric = PasswordRules.RequireNonAlphanumeric;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //  .AddEntityFrameworkStores<ApplicationDbContext>()
            //  .AddDefaultTokenProviders();


            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });



            //services.AddDbContext<HWIdentityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
            services.AddScoped<IUnitOfWork, UnitOfWork<HWIdentityContext>>();
            //services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddMvc();

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            //});


            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            .AddAspNetIdentity<ApplicationUser>()
            // this adds the config data from DB (clients, resources)
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            // this adds the operational data from DB (codes, tokens, consents)
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));

                // this enables automatic token cleanup. this is optional.
                options.EnableTokenCleanup = true;
                // options.TokenCleanupInterval = 15; // frequency in seconds to cleanup stale grants. 15 is useful during debugging
            });

            // builder.AddSigningCredential();
            // builder.addt();

            builder.AddDeveloperSigningCredential(false, "signcre");


            //if (Environment.IsDevelopment())
            //{
            //    builder.AddDeveloperSigningCredential();
            //}
            //else
            //{
            //    throw new Exception("need to configure key material");
            //}

            if (Environment.IsDevelopment())
            {

                try
                {
                    var rabbitMqConfig = Configuration.GetSection(typeof(SigninKeyCredentials).Name).Get<SigninKeyCredentials>();
                    var keyFilePath = rabbitMqConfig.KeyFilePath;
                    var keyFilePassword = rabbitMqConfig.KeyFilePassword;
                    var filePath = Environment.WebRootPath + keyFilePath;
                    if (File.Exists(filePath))
                    {
                        _logger.LogDebug($"SigninCredentialExtension adding key from file {keyFilePath}");
                        builder.AddSigningCredential(new X509Certificate2(filePath, "", X509KeyStorageFlags.MachineKeySet));
                    }
                    else
                    {
                        _logger.LogError($"SigninCredentialExtension cannot find key file {filePath}");
                    }
                }
                catch (Exception ex)
                {
                    // added this to prevent exception from Certificate invalid password.
                }
            }
            else
            {
                var keyIssuer = "IdentityServerCN";
                //logger.LogDebug($"SigninCredentialExtension adding key from store by {keyIssuer}");
                X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);
                var certificates = store.Certificates.Find(X509FindType.FindByIssuerName, keyIssuer, true);

                if (certificates.Count > 0)
                    builder.AddSigningCredential(certificates[0]);
                else
                {
                    _logger.LogError("A matching key couldn't be found in the store");
                }

            }

            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddSingleton(Configuration.GetSection("FacebookCredentials").Get<FbCredentials>());

            services.AddScoped<IExceptionService, ExceptionService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            //Inject the classes we just created
            services.AddScoped<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddScoped<IProfileService, ProfileService>();
            // services.AddScoped<ILogger, LoggerMessage>();

            //my user repository
            services.AddScoped<IUserRepository, UserRepository>();


            string ElmahConnectionString = Configuration.GetConnectionString("ElmahConnectionString");

            services.AddElmah<SqlErrorLog>(options =>
            {
                options.ConnectionString = ElmahConnectionString;
            });
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
            }

            app.UseElmah();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "api/{controller}/{action}/{id?}");
                endpoints.MapControllerRoute(name: "default-start", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
