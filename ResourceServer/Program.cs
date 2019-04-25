using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;
using ResourceServer.Migrations;

namespace ResourceServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            //CreateWebHostBuilder(args).Build();
            //Migration.createDB();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
<<<<<<< HEAD
                .UseKestrel(options =>                              // overwrites Kestrel addresses to only enable HTTP
                {
                    options.Listen(IPAddress.Any, 80);         // http:*:80
                })
<<<<<<< HEAD
=======
                //.UseKestrel(options =>                              // overwrites Kestrel addresses to only enable HTTP
                //{
                //    options.Listen(IPAddress.Any, 80);         // http:*:80
                //})
>>>>>>> 6114ad476b13f28b615b7ce6ba851e6a8616d6a3
                .UseStartup<Startup>();
=======
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;

                    var logConfigPath = "nlog.config";

                    if (env.IsDevelopment())
                        logConfigPath = "nlog.Development.config";

                    else if (env.IsStaging())
                        logConfigPath = "nlog.Staging.config";

                    env.ConfigureNLog(logConfigPath);
                })
                .UseStartup<Startup>()
                .UseNLog();
>>>>>>> origin/release/dev
    }
}
