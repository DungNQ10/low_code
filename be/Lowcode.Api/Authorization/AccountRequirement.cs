using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lowcode.Domain.Services.RolesFacade;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Lowcode.Api.Modules;
using System.Text;

namespace Lowcode.Api.Authorization
{
    public class PermissionRequirement : AuthorizationHandler<PermissionRequirement>, IAuthorizationRequirement
    {
        public PermissionCode Permission { get; }

        public PermissionRequirement(PermissionCode permission)
        {
            Permission = permission;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            // Your logic here... or anything else you need to do.
            //if (context.User.IsInRole("Agent"))
            //{
            //    context.Succeed(requirement);
            //    return;
            //}

            var filterContext = context.Resource as AuthorizationFilterContext;

            if (context.User == null)
            {
                // no user authorizedd. Alternatively call context.Fail() to ensure a failure 
                // as another handler for this requirement may succeed

                filterContext.Result = new ContentResult()
                {
                    StatusCode = 401,
                    ContentType = "application/json",
                    Content = JsonConvert.SerializeObject(new ResultBase<bool>()
                    {
                        success = false,
                        message = "Tài khoản chưa được xác thực",
                        status_code = 401
                    })
                };
                context.Fail();
            }

            var myPermission = context.User.Claims.FirstOrDefault(c => c.Type == "permissions");
            if (myPermission == null)
            {

                filterContext.Result = new ContentResult()
                {
                    StatusCode = 403,
                    ContentType = "application/json",
                    Content = JsonConvert.SerializeObject(new ResultBase<bool>()
                    {
                        success = false,
                        message = "Tài khoản chưa được phân quyền",
                        status_code = 403
                    })
                };

                context.Fail();
            }

            bool hasPermission = myPermission.Value.Contains(";" + requirement.Permission.GetHashCode() + ";");
            if (hasPermission)
            {
                context.Succeed(requirement);
            }
            else
            {
                filterContext.Result = new ContentResult()
                {
                    StatusCode = 403,
                    ContentType = "application/json",
                    Content = JsonConvert.SerializeObject(new ResultBase<bool>()
                    {
                        success = false,
                        message = $"Bạn không có quyền '{Permission.GetAttributeOfType<PermissionDetailsAttribute>().Name}'",
                        status_code = 403
                    })
                };

                //var response = filterContext?.HttpContext.Response;

                //response?.OnStarting(async () =>
                //{
                //    response.ContentType = "application/json";

                //    filterContext.HttpContext.Response.StatusCode = 403;

                //    var strMsg = JsonConvert.SerializeObject(new ResultBase<bool>()
                //    {
                //        success = false,
                //        message = $"Bạn không có quyền '{Permission.GetAttributeOfType<PermissionDetailsAttribute>().Name}'",
                //        status_code = 403
                //    });

                //    byte[] message = Encoding.UTF8.GetBytes(strMsg);

                //    await response.Body.WriteAsync(message, 0, message.Length);
                //});

                context.Fail();
            }
        }
    }
}
