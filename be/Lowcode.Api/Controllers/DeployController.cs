using System;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Lowcode.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DeployController : ControllerBase
    {
        public ActionResult<string> Deploy()
        {
            System.Diagnostics.Process.Start(@"C:\Deployments\CT\API-RELEASE.bat");

            return "API begin deploying...";
        }
    }
}
