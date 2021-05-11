using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

using Lowcode.Domain.Services.UserFacade;

namespace Lowcode.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("/")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : ControllerBase
    {
        private readonly IUserService userService;

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [HttpGet("/api")]
        public ActionResult<string> Get()
        {
            //var user = userService.GetByIds(new System.Collections.Generic.List<long> { 1, 3 });
            //Log.Error("Logged at {TimeStamp}", DateTime.Now.ToLongTimeString());
            return "WGD PMS API SERVER V1";
        }
    }
}
