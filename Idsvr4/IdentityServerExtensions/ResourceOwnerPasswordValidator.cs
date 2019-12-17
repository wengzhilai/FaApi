using Helper;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.Configuration;
using Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Idsvr4.IdentityServerExtensions
{
    /// <summary>
    ///ResourceOwnerPassword模式下用于自定义登录验证 
    /// </summary>
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IConfiguration config;

        public ResourceOwnerPasswordValidator(IConfiguration config
            )
        {
            this.config = config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            string userObjJson = context.Request.Raw.Get("userObjJson");
            await Task.Run(()=> { 
            
            });
            //根据context.UserName和context.Password与数据库的数据做校验，判断是否合法
            try
            {

                FaUserEntity userObj = TypeChange.JsonToObject<FaUserEntity>(userObjJson);

                if (userObj != null)
                {

                    List<Claim> claimList = new List<Claim>();

                    claimList.Add(new Claim(JwtClaimTypes.Id, userObj.id.ToString()));
                    if (userObj.roleIdList != null) claimList.Add(new Claim(JwtClaimTypes.Role, string.Join(",", userObj.roleIdList)));
                    claimList.Add(new Claim(JwtClaimTypes.Name, userObj.name));
                    claimList.Add(new Claim(JwtClaimTypes.PhoneNumber, userObj.loginName));
                    claimList.Add(new Claim(JwtClaimTypes.Role, "superadmin"));

                    context.Result = new GrantValidationResult(
                     subject: context.UserName,
                     authenticationMethod: OidcConstants.AuthenticationMethods.Password,
                     DateTime.Now.AddDays(1),
                     claims: claimList);
                }
                else
                {
                    //验证失败
                    context.Result = new GrantValidationResult(
                        TokenRequestErrors.InvalidGrant,
                        "loginError"
                        );
                }
            }
            catch (Exception e)
            {
                //验证失败
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    e.Message
                    );
            }
        }
    }
}
