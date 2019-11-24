using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Idsvr4.IdentityServerExtensions
{

    /// <summary>
    /// 自定义用户登录返回的信息claims
    /// </summary>
    public class ProfileService : IProfileService
    {
        private readonly ILogger logger;

        public ProfileService(ILogger<ProfileService> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        ///  只要有关用户的身份信息单元被请求（例如在令牌创建期间或通过用户信息终点），就会调用此方法
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {

            ////判断是否有请求Claim信息
            //if (context.RequestedClaimTypes.Any())
            //{
            //    //根据用户唯一标识查找用户信息
            //    var user = Users.FindBySubjectId(context.Subject.GetSubjectId());
            //    if (user != null)
            //    {
            //        //调用此方法以后内部会进行过滤，只将用户请求的Claim加入到 context.IssuedClaims 集合中 这样我们的请求方便能正常获取到所需Claim

            //        context.AddRequestedClaims(user.Claims);
            //    }
            //}


            try
            {
                var claims = context.Subject.Claims.ToList();

                context.IssuedClaims = claims.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
            }
        }



        public static (bool reuslt, string errorMessage) Validation()
        {

            return (false, "");
        }

        public (string, string, int, double) FunctionName(string ID)
        {
            string a1 = "";    //第1个返回值
            string a2 = "";    //第2个返回值
            int a3 = 1;        //第3个返回值
            double a4 = 1.20;  //第4个返回值
            return (a1, a2, a3, a4);
        }



        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
        }
    }
}
