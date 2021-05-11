using Microsoft.Extensions.DependencyInjection;
using Lowcode.Domain.Services.RolesFacade;
using Lowcode.Domain.Services.RolesFacade.Implementation;
using Lowcode.Domain.Services.UserFacade;
using Lowcode.Domain.Services.UserFacade.Implementation;
using Lowcode.Framework.Data;
using Microsoft.Extensions.Configuration;

using Lowcode.Domain.Services.AttachmentsFacade;
using Lowcode.Domain.Services.AttachmentsFacade.Implementation;
using Lowcode.Domain.Modules.CodeIt.Services;
using Lowcode.Domain.Services.DynamicFormFacade;
using Lowcode.Domain.Services.DynamicFormFacade.Implementation;
using Lowcode.Domain.Services.CountryFacade;
using Lowcode.Domain.Services.CountryFacade.Implementation;

namespace Lowcode.Api.Configuration
{
    public static class DIConfigurator
    {
        public static void ConfigDI(this IServiceCollection services, IConfiguration configuration)
        {
            //services
            //    .AddEntityFrameworkSqlServer()
            //    .AddDbContext<DomainContext>((serviceProvider, options) =>
            //                 options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            //                 .UseInternalServiceProvider(serviceProvider), ServiceLifetime.Singleton);

            services.AddScoped(typeof(IEntityRepository<,>), typeof(EntityRepository<,>));
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IAttachmentsService, AttachmentsService>();
            services.AddScoped<IExtractDbService, ExtractDbService>();
            services.AddScoped<IDynamicFormService, DynamicFormService>();
            services.AddScoped<ICountryService, CountryService>();
        }
    }
}
