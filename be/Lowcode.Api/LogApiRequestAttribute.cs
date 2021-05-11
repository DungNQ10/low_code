
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

using Lowcode.Api.Helpers;
using Lowcode.Domain.Services.UserFacade;

namespace Lowcode.WebApi.Logger
{
    public class LogApiRequestAttribute : ActionFilterAttribute
    {

        IUserService _userService;
       
         private readonly IHttpContextAccessor httpContextAccessor;


        public LogApiRequestAttribute(IUserService userService, IHttpContextAccessor httpContextAccessor
            )
        {

            _userService = userService;
            
            this.httpContextAccessor = httpContextAccessor;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log(filterContext);
            base.OnActionExecuting(filterContext);
        }

        private void Log(ActionExecutingContext actionExecutedContext)
        {
            if (!actionExecutedContext.HttpContext.User.Identity.IsAuthenticated)
                return;

            CheckTimeAccess(actionExecutedContext);

        }

        private void CheckTimeAccess(ActionExecutingContext context)
        {
            //check time access

            var id = context.HttpContext.User.Identity.GetUserId();
            //check ip accesss
            var clientIp = context.HttpContext.Connection.RemoteIpAddress.ToString();
            Serilog.Log.Logger.Information("clientIP:" + clientIp);
            Serilog.Log.Logger.Information("clientIP 2:" + httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString());
            var listIPs = new List<string>();
            var user = _userService.GetById(id);
            if (user != null)
            {
                if (!string.IsNullOrWhiteSpace(user.WhiteList))
                {
                     listIPs = user.WhiteList.Split(
     new[] { "\r\n", "\r", "\n" },
     StringSplitOptions.RemoveEmptyEntries
 ).ToList();
                   
                }
            }


            if (listIPs.Count > 0 && listIPs.Contains(clientIp))
            {
                return;
            }
            else
            {
               
                if(listIPs.Count>0)
                    throw new InvailidAccessException("Bạn không được phép truy cập hệ thống qua địa chỉ này.");
            }
            
        }


    }
}