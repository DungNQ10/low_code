using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using IdentityServer4.Services;
using IdentityServer4.Validation;

using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Lowcode.Authentication.Configuration;
using Lowcode.Authentication.IdentityServer;
using Lowcode.Domain.Services;

namespace Lowcode.Authentication
{
    public class Startup
    {
        public IConfiguration _configuration { get; private set; }
        public IHostingEnvironment _environment { get; private set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration;
            _environment = env;

            DomainMaps.Config();

            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();

            _configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(_configuration)
                        .CreateLogger();
            Log.Logger.Information("ENV:" + env.EnvironmentName);
            Log.Logger.Information("ConnectionString:" +_configuration["ConnectionStrings:DefaultConnection"]);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc();
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add our Config object so it can be injected
            var appSettingSection = _configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);
            var appSettings = new AppSettings();
            appSettingSection.Bind(appSettings);
            foreach (var item in appSettings.ClientAppRedirectUri)
            {
                Log.Logger.Information("APPSETTING:" + item);
            }
           
            // CONFIGURE IDENTITY SERVER 4 WITH IN-MEMORY STORES, KEYS, CLIENTS AND SCOPES

            //1. Identity server 4 cert
            var cert = new X509Certificate2(Path.Combine(_environment.ContentRootPath, "IdentityServer", "idsrv3test.pfx"), "idsrv3test");

            //2. Inject storage user service
            services.ConfigDI(_configuration);

            //3. Add certificates
            services.AddIdentityServer()
                .AddSigningCredential(cert)
                .AddInMemoryIdentityResources(Config.GetIdentityResources()) //check below
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients(appSettings))
                .AddProfileService<ProfileService>();

            //3. Inject the classes we just created
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ProfileService>();

            ////Config allow cross origin    
            services.AddCors();
            services.AddAntiforgery(x => x.HeaderName = "X-XSRF-TOKEN");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseIdentityServer();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            // Preceding code ommitted.
            app.UseCors(builder => builder
                   .AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials());

            app.Use(next => context =>
            {
                string path = context.Request.Path.Value;

                // The request token can be sent as a JavaScript-readable cookie, 
                // and Angular uses it by default.
                var tokens = antiforgery.GetAndStoreTokens(context);
                context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
                    new CookieOptions() { HttpOnly = false });

                return next(context);
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
