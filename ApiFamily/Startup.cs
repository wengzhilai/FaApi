
using Autofac;
using AutoMapper;
using Helper;
using IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Repository;
using System.Reflection;

namespace ApiFamily
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;

            Configuration.Bind(AppSettingsManager.self);

        }


        public void ConfigureServices(IServiceCollection services)
        {

            #region 注入
            services.TryAddSingleton<IFamilyRepository, FamilyRepository>();
            services.TryAddSingleton<IUserInfoRepository, UserInfoRepository>();
            
            #endregion

            services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
            {
                options.RequireHttpsMetadata = false;
                options.Authority = AppSettingsManager.self.Idsvr4Url;
                options.Audience = "FamilyService";
            }
            );

            #region  添加SwaggerUI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "用户文档",
                    Version = "v1",
                    Description = "用于前端和后端调用",
                    Contact = new OpenApiContact
                    {
                        Name = "翁志来",
                        Email = "3188894@qq.com"
                    }
                });

                options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                options.IncludeXmlComments(WebHostEnvironment.ContentRootPath + "/ApiFamily.xml");

            });

            #endregion

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                // 忽略循环引用
                // 不使用驼峰
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                // 设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                // 如字段为null值，该字段不会返回到前端
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
            services.AddAutoMapper(typeof(Startup));


            services.AddCors(options =>
            {
                options.AddPolicy("AllowSameDomain",
                    builder =>
                    {
                        builder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                    });
            });


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();
            app.UseCors("AllowSameDomain");
            app.UseAuthentication();//认证
            app.UseAuthorization();//授权
   
            #region 使用SwaggerUI
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ETC接口文档");
                // 访问Swagger的路由后缀
                options.RoutePrefix = "swagger";
            });
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("ApiFamily");
                });
                endpoints.MapControllers();
            });

        }
    }
}

