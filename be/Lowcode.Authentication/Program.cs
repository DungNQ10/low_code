﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Lowcode.Authentication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Log.Logger = new LoggerConfiguration()
            //  .MinimumLevel.Debug()
            //  .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            //  .MinimumLevel.Override("System", LogEventLevel.Warning)
            //  .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
            //  .Enrich.FromLogContext()
            //  .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
            //  .WriteTo.File("D:\\PRODUCTS\\PCC1 PMS\\Development\\ZB_CLOUD\\Lowcode.Authentication\\Logs\\log.txt", LogEventLevel.Error)
            //  .CreateLogger();

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
    }
}