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
                    //���������Դ
                    .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
                    //����API��Դ
                    .AddInMemoryApiResources(IdentityConfig.GetApiResources())
                    //Ԥ��Client
                    .AddInMemoryClients(IdentityConfig.GetClients())
                    //�Զ����¼������Ϣ
                    .AddProfileService<ProfileService>()
                    //���Passwordģʽ�������Զ����¼��֤ 
                    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                    //����Զ�����Ȩģʽ
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
            app.UseIdentityServer();//ʹ��IdentityServer
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
