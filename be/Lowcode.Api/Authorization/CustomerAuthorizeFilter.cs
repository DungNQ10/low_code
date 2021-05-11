using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lowcode.Domain.Services.UserFacade;
using Lowcode.Api.Helpers;
namespace Lowcode.Api.Authorization
{
    public class CustomerAuthorizeFilter : AuthorizeFilter
    {
        IServiceCollection services;
        public CustomerAuthorizeFilter(AuthorizationPolicy policy, IServiceCollection services) : base(policy)
        {
            this.services = services;
        }

        public override Task OnAuthorizationAsync(Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext context)
        {
            // If there is another authorize filter, do nothing
            if (context.Filters.Any(item => item is IAsyncAuthorizationFilter && item.GetType() == typeof(Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter)))
            {
                var timeStemp = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "timestemp");
                if (timeStemp == null)
                {
                    context.Result = new UnauthorizedResult();
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(timeStemp.Value))
                    {
                        context.Result = new UnauthorizedResult();
                    }
                    else
                    {
                        var sp = services.BuildServiceProvider();
                        var userService = sp.GetService<IUserService>();
                        var checkModified = userService.CheckModified(timeStemp.Value, context.HttpContext.User.Identity.GetUserId());
                        if (checkModified)
                        {
                            context.Result = new UnauthorizedResult();
                        }
                    }
                }

                return Task.FromResult(0);
            }

            //Otherwise apply this policy
            return Task.FromResult(0);
        }
    }
    }
