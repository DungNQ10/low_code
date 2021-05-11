using Microsoft.Extensions.DependencyInjection;
using Lowcode.Domain.Services.RolesFacade;
using Lowcode.Domain.Services.RolesFacade.Implementation;
using Lowcode.Domain.Services.UserFacade;
using Lowcode.Domain.Services.UserFacade.Implementation;
using Lowcode.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lowcode.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Lowcode.Domain.Services.AttachmentsFacade;
using Lowcode.Domain.Services.AttachmentsFacade.Implementation;

namespace Lowcode.Authentication.Configuration
{
    public static class DIConfigurator
    {
        public static void ConfigDI(this IServiceCollection services, IConfiguration configuration)
        {
            //services
            //    .AddEntityFrameworkSqlServer()
            //    .AddDbContext<DomainContext>((serviceProvider, options) =>
            //                 options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            //                 .UseInternalServiceProvider(serviceProvider));

            //services.AddScoped(typeof(IAppContextFactory<>), typeof(AppContextFactory<>));
            services.AddScoped(typeof(IEntityRepository<,>), typeof(EntityRepository<,>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IAttachmentsService, AttachmentsService>();
        }
    }
}
