using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ApiUser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory());

                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                     {
                         config
                             .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                             .AddJsonFile("appsettings.json", true, true)
                             .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                             .AddJsonFile("hosts.json", false, false)
                             .AddEnvironmentVariables();
                     });
                    webBuilder.UseStartup<Startup>();
                })
                //����Autofac
                .UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
