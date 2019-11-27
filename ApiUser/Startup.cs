
using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApiUser
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
            //IdentityServer
            services.AddAuthentication("Bearer")
               .AddIdentityServerAuthentication(options =>
               {
                   options.RequireHttpsMetadata = false;
                   options.Authority = "http://localhost:9001";
                   options.ApiName = "UsersService";
               });

            services.AddControllers(options =>
            {
                //options.Filters.Add(typeof(AuthorizeAttribute));
                //options.Filters.Add(new RequireHttpsAttribute());

                //var policy = new AuthorizationPolicyBuilder()
                // .RequireAuthenticatedUser()
                // .RequireRole("superadmin")
                // .Build();
                //options.Filters.Add(new AuthorizeFilter(policy));
            });
            //添加HTTP请求
            services.AddHttpClient();

            //
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
            Configuration.Bind(AppSettingsManager.self);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();
            app.UseCors("AllowSameDomain");
            app.UseAuthorization();
            app.UseAuthentication();
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
