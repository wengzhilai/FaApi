using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using WebApi.Comon;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using System.Reflection;
using WebApi.Unit;
using Helper;

namespace WebApi
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public Startup(IConfiguration configuration,IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        
        public IConfiguration Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        /// <summary>
        /// 让第三方容器接管Core 的默认DI
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //初始化注入IOptions<T>
            // services.AddOptions();
            //自动初始化MongoSettings实例并且映射MongodbHost里的配置
            services.Configure<MongodbHost>(Configuration.GetSection("MongoSettings"));
            Configuration.Bind("MongoSettings", AppSettingsManager.MongoSettings);

            #region JWT认证
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            Configuration.Bind("JwtSettings", AppSettingsManager.JwtSettings);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = AppSettingsManager.JwtSettings.Audience,
                    ValidIssuer = AppSettingsManager.JwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingsManager.JwtSettings.SecretKey))
                };
            });
            
            #endregion
            services.AddMvc();
            
            #region  添加SwaggerUI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "Dinner API接口文档",
                    Version = "v1",
                    Description = "RESTful API for Dinner",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "wangshibang", Email = "wangyulong0505@sina.com", Url = "" }
                });
                options.IgnoreObsoleteActions();
                options.DocInclusionPredicate((docName, description) => true);
                options.IncludeXmlComments(_hostingEnvironment.ContentRootPath+"/bin/Debug/netcoreapp2.2/WebApi.xml");
                options.DescribeAllEnumsAsStrings();
                options.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
            });
            #endregion
            #region 依赖注入

            var builder = new ContainerBuilder();//实例化容器
            //注册所有模块module
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            //获取所有的程序集
            // var assemblys = RuntimeHelper.GetAllAssemblies().ToArray();
            Assembly amy = Assembly.LoadFrom(_hostingEnvironment.ContentRootPath+"/../Repository/bin/Debug/netstandard2.0/Repository.dll"); 
            //注册仓储，所有IRepository接口到Repository的映射
            builder.RegisterAssemblyTypes(amy).Where(t => t.Name.EndsWith("Repository") && !t.Name.StartsWith("I")).AsImplementedInterfaces();
            //注册服务，所有IApplicationService到ApplicationService的映射
            builder.Populate(services);
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer); //第三方IOC接管 core内置DI容器 
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
   
            //请求错误提示配置
            app.UseErrorHandling();
            
            // app.UseAuthentication();//启用验证

            #region 使用SwaggerUI

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Dinner API V1");
            });

            #endregion

            // app.UseHttpsRedirection();
            
            app.UseMvc();

        }
    }
}
