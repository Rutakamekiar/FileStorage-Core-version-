// <copyright company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace FileStorage.WebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                          .UseStartup<Startup>()
                          .ConfigureLogging(logging =>
                          {
                              logging.AddConsole();
                              logging.SetMinimumLevel(LogLevel.Trace);
                          })
                          .UseNLog()
                          .UseKestrel(options =>
                          {
                              options.Limits.MaxRequestBodySize = 10 * 1024;
                              options.Limits.MinRequestBodyDataRate =
                                  new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                              options.Limits.MinResponseDataRate =
                                  new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                          })
                          .UseIISIntegration();
        }
    }
}