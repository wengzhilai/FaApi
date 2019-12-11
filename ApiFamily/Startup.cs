
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
            #endregion

            services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
            {
                options.RequireHttpsMetadata = false;
                options.Authority = AppSettingsManager.self.Idsvr4Url;
                options.Audience = "FamilyService";
            }
            );

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
                            //.WithOrigins("http://localhost:8100")
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
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapControllers();
            });
        }
    }
}

