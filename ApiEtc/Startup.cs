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
        /// 读取配置
        /// </summary>
        /// <value></value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 加载日志资源
        /// </summary>
        /// <value></value>
        public ILoggerRepository repository { get; set; }
        /// <summary>
        /// 环境
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
            Configuration.Bind("RedisConfig", AppSettingsManager.self.RedisConfig);

            Configuration.Bind("WebConfig", AppConfig.WebConfig);
            Configuration.Bind("WeiXin", AppConfig.WeiXin);

            System.Console.WriteLine(AppConfig.WebConfig.ClientPrice);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            #region 注入
            services.TryAddSingleton<IStaff, StaffRepository>();
            services.TryAddSingleton<IClient, ClientRepository>();
            services.TryAddSingleton<IWeixin, WeixinRepository>();
            services.TryAddSingleton<IWallet, WalletRepository>();
            services.TryAddSingleton<IApplicants, ApplicantsRepository>();
            services.TryAddSingleton<Helper.Query.IQuery, Helper.Query.QueryRepository>();
            #endregion

            #region  添加SwaggerUI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ETC文档",
                    Version = "v1",
                    Description = "用于前端和后端调用",
                    Contact = new OpenApiContact { Name = "翁志来", Email = "3188894@qq.com" }
                });
                options.IgnoreObsoleteActions();
                options.DocInclusionPredicate((docName, description) => true);
                options.IncludeXmlComments(WebHostEnvironment.ContentRootPath + "/ApiEtc.xml");
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                options.AddSecurityDefinition("JWT授权", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
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
                // 忽略循环引用
                // 不使用驼峰
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                // 设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                // 如字段为null值，该字段不会返回到前端
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //添加静态文件
            app.UseStaticFiles();

            #region 使用SwaggerUI

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ETC接口文档");
            });

            #endregion

            //app.UseHttpsRedirection();

            app.UseRouting();
            // 跨域必须要 routing后面
            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ETC接口文档");
                    // 访问Swagger的路由后缀
                    options.RoutePrefix = "sw";
                });
                endpoints.MapControllers();
            });
        }
    }
}
