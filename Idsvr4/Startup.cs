using Autofac;
using Helper;
using Idsvr4.Configuration;
using Idsvr4.IdentityServerExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace Idsvr4
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            Configuration.Bind(AppSettingsManager.self);


            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    //配置身份资源
                    .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
                    //配置API资源
                    .AddInMemoryApiResources(IdentityConfig.GetApiResources())
                    //预置Client
                    .AddInMemoryClients(IdentityConfig.GetClients())
                    //自定义登录返回信息
                    .AddProfileService<ProfileService>()
                    //添加Password模式下用于自定义登录验证 
                    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                    //添加自定义授权模式
                    .AddExtensionGrantValidator<SMSGrantValidator>()
                    ;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();
            app.UseIdentityServer();//使用IdentityServer
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("IdentityServer4");
                });

            });
        }
    }
}
