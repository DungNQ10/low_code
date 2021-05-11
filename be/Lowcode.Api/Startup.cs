using System;
using System.Collections.Generic;
using System.IO;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using Lowcode.Api.Authorization;
using Lowcode.Api.Configuration;
using Lowcode.Api.Helpers;

using Lowcode.Domain.Services.RolesFacade;
using Lowcode.Domain.Services.RolesFacade.Implementation;
using Lowcode.Domain.Services;
using Lowcode.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Globalization;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Lowcode.WebApi.Logger;
using Microsoft.AspNetCore.HttpOverrides;
using Lowcode.Domain.Shared;
using Lowcode.Framework.Utils;

namespace Lowcode.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private AppSettings appSettings;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Domain.Services.DomainMaps.Config();

            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();
            
            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(Configuration)
                        //.WriteTo.Sentry("https://f65f908fde9940f0b2b16c2045643ea9@sentry.io/1260343")
                        .Enrich.FromLogContext()
                        .CreateLogger();
            Log.Logger.Information("ENV:" + env.EnvironmentName);
          
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services
                .AddMvc(config =>
                {
                    // Add to global model binders so you don't need to use the [ModelBinder] attribute.
                    config.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());

                    config.Filters.Add(typeof(GlobalExceptionFilter));
                    config.Filters.Add(new CustomerAuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(), services));
                    config.Filters.Add(typeof(LogApiRequestAttribute));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                });

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info { Title = "VNL SMART Api v1", Version = "V1" });
                config.IncludeXmlComments(string.Format("{0}\\Lowcode.Api.xml", AppDomain.CurrentDomain.BaseDirectory));
                config.IncludeXmlComments(string.Format("{0}\\Lowcode.Domain.xml", AppDomain.CurrentDomain.BaseDirectory));

                config.DescribeAllEnumsAsStrings();
                config.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "Authorization header using the Bearer scheme",
                    Name = "Authorization",

                    In = "header"
                });
                config.DocumentFilter<SwaggerSecurityRequirementsDocumentFilter>();
                //config.CustomSchemaIds(x => x.FullName);
            });

            // Add our Config object so it can be injected
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);
            appSettings = new AppSettings();
            appSettingSection.Bind(appSettings);
            Constant.UploadFolder = appSettings.UploadFolder;
          
            Log.Logger.Information("authenticationserver:" + appSettings.AuthenticationServer);
            //add fix authority for lan
            //var authority = appSettings.AuthenticationServer;
            //if (requestHost == "192.168.100.8")
            //{
            //    authority = "http://192.168.100.8:82";
            //}

            services.AddAuthentication("Bearer")
                   
                   .AddIdentityServerAuthentication(options =>
                   {
                       options.Authority = appSettings.AuthenticationServer;
                       options.RequireHttpsMetadata = false;
                       options.ApiName = "wgd_api";
                       //options.SupportedTokens = IdentityServer4.AccessTokenValidation.SupportedTokens.Both;
                       //options.ApiSecret = "secret";
                   });

            

            //Authorize user access
            services.AddAuthorization(options =>
            {
                var permissions = RolesService.ListPermissions();
                var all = new List<Permission>();
                permissions.ForEach(k => all.AddRange(k.Permissions));
                foreach (var permission in all)
                {
                    var policyName = permission.Code.ToString();
                    options.AddPolicy(policyName,
                        policy => policy.Requirements.Add(new PermissionRequirement(permission.Code)));
                }
            });
            services.AddCors();
            //Inject storage user service
            services.ConfigDI(Configuration);

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("vi-VN");
                options.SupportedCultures = new List<CultureInfo>
                        {
                            new CultureInfo("vi-VN"),
                            new CultureInfo("en-US")
                        };
                options.SupportedUICultures = new List<CultureInfo>
                        {
                            new CultureInfo("vi"),
                            new CultureInfo("en")
                        };
            });

            DocxUtils.RegisterAsposeWord();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.AddSentryContext();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder => builder
                   //.WithOrigins(appSettings.ClientAppRedirectUri)
                   .AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials());

            app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                ForwardedHeaders.XForwardedProto
            });
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(new CultureInfo("vi-VN")),
                SupportedCultures = new List<CultureInfo>
                        {
                            new CultureInfo("vi-VN"),
                            new CultureInfo("en-US")
                        },
                SupportedUICultures = new List<CultureInfo>
                        {
                            new CultureInfo("vi"),
                            new CultureInfo("en")
                        }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //implement docs for api
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("v1/swagger.json", "VNL SMART Api v1");
                config.DocExpansion(DocExpansion.None);
                config.ShowExtensions();
            });
        }
    }

    public class SwaggerSecurityRequirementsDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument document, DocumentFilterContext context)
        {
            document.Security = new List<IDictionary<string, IEnumerable<string>>>()
            {
                new Dictionary<string, IEnumerable<string>>()
                {
                    { "Bearer", new string[]{ } },
                    { "Basic", new string[]{ } },
                }
            };
        }
    }
}
