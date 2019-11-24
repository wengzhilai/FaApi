using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ApiUser.Configuration;
using ApiUser.IdentityServerExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApiUser
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
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
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
