using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ApiUser
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
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
                             .AddEnvironmentVariables();
                     });
                    webBuilder.UseStartup<Startup>();
                })
            //Ìí¼ÓAutofac
            ;
    }
}
