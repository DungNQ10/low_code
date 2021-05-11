using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Lowcode.Api.Modules;
using Serilog;

namespace Lowcode.Api.Helpers
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //var exceptionType = context.Exception.GetType();
            //if (exceptionType == typeof(ServiceException))
            //{
            //    string message = context.Exception.Message;
            //    var status = HttpStatusCode.OK;

            //    context.ExceptionHandled = true;
            //    HttpResponse response = context.HttpContext.Response;
            //    response.StatusCode = (int)status;
            //    response.ContentType = "application/json";
            //    var err = new { success = false, message };
            //    response.WriteAsync(JsonConvert.SerializeObject(err));
            //}
            //else
            {
                //log bug
                Log.Logger.Fatal(context.Exception.ToString());

                var status = HttpStatusCode.OK;
                context.ExceptionHandled = true;
                HttpResponse response = context.HttpContext.Response;
                response.StatusCode = (int)status;
                response.ContentType = "application/json";
                if (context.Exception is InvailidAccessException )
                {
                    var err = new { success = false, message = context.Exception.Message, status_code = 403 };
                    response.WriteAsync(JsonConvert.SerializeObject(err));
                }
                else
                {
                    var err = new { success = false, message = context.Exception.Message, status_code = 500 };
                    response.WriteAsync(JsonConvert.SerializeObject(err));
                }
              
            }
        }
    }
}
