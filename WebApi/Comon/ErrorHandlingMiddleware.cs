
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WebApi.Comon
{
    /// <summary>
    /// 处理出错的中间件
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// 处理出错的中间件
        /// </summary>
        /// <param name="next"></param>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var statusCode = context.Response.StatusCode;
                if (ex is ArgumentException)
                {
                    statusCode = 200;
                }
                HandleExceptionAsync(context, statusCode, ex.Message);
            }
            finally
            {
                var statusCode = context.Response.StatusCode;
                var msg = "";
                if (statusCode == 401)
                {
                    msg = "未授权";
                }
                else if (statusCode == 404)
                {
                    msg = "未找到服务";
                }
                else if (statusCode == 502)
                {
                    msg = "请求错误";
                }
                else if (statusCode == 400)
                {
                    msg = "参数验证不通过";
                }
                else if (statusCode != 200)
                {
                    msg = "未知错误";
                }
                
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    HandleExceptionAsync(context, statusCode, msg);
                }
            }
        }
　　　　 //异常错误信息捕获，将错误信息用Json方式返回
        private static void HandleExceptionAsync(HttpContext context, int statusCode, string msg)
        {
            //var result = JsonConvert.SerializeObject(new Result() { IsSuccess=false,Msg=msg,Code= statusCode.ToString() });
            //context.Response.ContentType = "application/json;charset=utf-8";
            //return context.Response.WriteAsync(result);
        }
    }
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ErrorHandlingExtensions
    {
        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}