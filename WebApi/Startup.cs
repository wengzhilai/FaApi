using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Helper;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Swashbuckle.AspNetCore.Swagger;
using WebApi.Comon;
using WebApi.Unit;

namespace WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
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
        }

        public ILoggerRepository repository { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public IContainer ApplicationContainer { get; private set; }

        /// <summary>
        /// 让第三方容器接管Core 的默认DI
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public void ConfigureServices(IServiceCollection services)
        {
            //初始化注入IOptions<T>
            // services.AddOptions();
            Configuration.Bind(AppSettingsManager.self);

            Configuration.Bind("JwtSettings", AppSettingsManager.self.JwtSettings);


            #region JWT认证
            //Bearer 

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = AppSettingsManager.self.JwtSettings.Audience,
                    ValidIssuer = AppSettingsManager.self.JwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingsManager.self.JwtSettings.SecretKey))
                };

                config.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                     {
                         //    var token = context.Request.Headers["Authorization"];//修改默认的http headers
                         //    context.Token = token.FirstOrDefault();
                         return Task.CompletedTask;
                     },
                    OnAuthenticationFailed = context =>
                      {
                          var token = context.Request.Headers["Authorization"];
                          if (token.Count() > 0 && !String.IsNullOrEmpty(token[0]) && token[0].Length > 8)
                          {
                              var tokenStr = token[0].Substring(6).Trim();
                              try
                              {
                                  var t = new JwtSecurityTokenHandler().ReadJwtToken(tokenStr);
                                  if (t.ValidTo < DateTime.Now)
                                  {
                                      throw new Exception("登录超时");
                                      //   var userId = t.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                                  }
                              }
                              catch { }
                          }

                          return Task.CompletedTask;
                      },
                    OnTokenValidated = context =>
                      {
                          var tokenStr = ((context as TokenValidatedContext).SecurityToken as JwtSecurityToken).RawData;
                          if (string.IsNullOrEmpty(tokenStr))
                          {
                              context.Fail("toke无效");
                          }
                          else
                          {
                              var userIdObj = new JwtSecurityTokenHandler().ReadJwtToken(tokenStr).Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                              int userId = 0;
                              int.TryParse(userIdObj.Value, out userId);
                              //判断toke值跟redis的token是否相同
                              //   var redisToken = RedisRepository.UserTokenGet(userId).Result;
                              //   if (redisToken == null || !redisToken.Equals(tokenStr))
                              //   {
                              //       context.Fail("toke过期");
                              //   }
                          }
                          return Task.CompletedTask;
                      }

                };
            });

            #endregion
            // 添加Quartz任务监控
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();//注册ISchedulerFactory的实例。
            #region  添加SwaggerUI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Dinner API接口文档",
                    Version = "v1",
                    Description = "RESTful API for Dinner",
                    Contact = new OpenApiContact { Name = "wangshibang", Email = "wangyulong0505@sina.com" }
                });
                options.IgnoreObsoleteActions();
                options.DocInclusionPredicate((docName, description) => true);
                options.IncludeXmlComments(WebHostEnvironment.ContentRootPath + "/bin/Debug/netcoreapp3.0/WebApi.xml");
                //options.DescribeAllEnumsAsStrings();
                //options.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
            });
            #endregion


            services.AddAutoMapper(typeof(Startup));

            services.AddHttpContextAccessor();
            services.AddControllers();

            //services.AddCors(options => options.AddPolicy("AllowSameDomain",
            //x => x.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials()));

            

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            //var assemblys = RuntimeHelper.GetAllAssemblies().ToArray();
            //builder.RegisterAssemblyTypes(assemblys).Where(t => t.Name.EndsWith("Repository", StringComparison.Ordinal) && !t.Name.StartsWith("I", StringComparison.Ordinal)).AsImplementedInterfaces();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();


            //请求错误提示配置
            app.UseErrorHandling();

            app.UseAuthentication();//启用验证

            #region 使用SwaggerUI

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Dinner API V1");
            });

            #endregion

            // app.UseHttpsRedirection();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseCors("AllowSameDomain");

        }
    }
}
