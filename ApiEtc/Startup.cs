using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiEtc.Config;
using ApiEtc.Controllers.Interface;
using ApiEtc.Repository;
using Helper;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Filters;

namespace ApiEtc
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "AllowSameDomain";


        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ������־��Դ
        /// </summary>
        /// <value></value>
        public ILoggerRepository repository { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <value></value>
        public IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="webHostEnvironment"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
            //log4net
            repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(repository, new FileInfo("Config/log4net.config"));

            Configuration.Bind(AppSettingsManager.self);
            Configuration.Bind("WebConfig", AppConfig.WebConfig);
            System.Console.WriteLine(AppConfig.WebConfig.ClientPrice);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            #region ע��
            services.TryAddSingleton<IStaff, StaffRepository>();
            services.TryAddSingleton<IClient, ClientRepository>();
            services.TryAddSingleton<IWallet, WalletRepository>();
            #endregion
            #region  ���SwaggerUI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ETC�ĵ�",
                    Version = "v1",
                    Description = "����ǰ�˺ͺ�˵���",
                    Contact = new OpenApiContact { Name = "��־��", Email = "3188894@qq.com" }
                });
                options.IgnoreObsoleteActions();
                options.DocInclusionPredicate((docName, description) => true);
                options.IncludeXmlComments(WebHostEnvironment.ContentRootPath + "/ApiEtc.xml");
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                options.AddSecurityDefinition("JWT��Ȩ", new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�\"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey
                });
            });
            #endregion

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                    });
            });

            services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                // ����ѭ������
                // ��ʹ���շ�
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                // ����ʱ���ʽ
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                // ���ֶ�Ϊnullֵ�����ֶβ��᷵�ص�ǰ��
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region ʹ��SwaggerUI

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ETC�ӿ��ĵ�");
            });

            #endregion

            //app.UseHttpsRedirection();

            app.UseRouting();
            // �������Ҫ routing����
            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    context.Response.Redirect("swagger/index.html");
                    //await context.Response.WriteAsync("Hello World!");

                });
                endpoints.MapControllers();
            });
        }
    }
}
