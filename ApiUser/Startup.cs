using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiUser.Configuration;
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
                    .AddDeveloperSigningCredential()//添加开发人员签名凭据
                    .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
                    .AddInMemoryApiResources(IdentityConfig.GetApiResources())//添加内存apiresource
                    .AddInMemoryClients(IdentityConfig.GetClients());//添加内存client
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
