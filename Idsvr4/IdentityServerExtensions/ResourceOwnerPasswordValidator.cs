using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using IRepository;
using Microsoft.Extensions.Configuration;
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
        private readonly ILoginRepository login;

        public ResourceOwnerPasswordValidator(IConfiguration config
            , ILoginRepository login
            )
        {
            this.config = config;
            this.login = login;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            string verificationCode = context.Request.Raw.Get("VerificationCode");

            //根据context.UserName和context.Password与数据库的数据做校验，判断是否合法
            try
            {


                var opObj = await login.UserLogin(new Models.LogingDto { LoginName = context.UserName, Password = context.Password });

                if (opObj.success && opObj.data != null)
                {

                    List<Claim> claimList = new List<Claim>();

                    claimList.Add(new Claim(JwtClaimTypes.Id, opObj.data.id.ToString()));
                    if (opObj.data.roleIdList != null) claimList.Add(new Claim(JwtClaimTypes.Role, string.Join(",", opObj.data.roleIdList)));
                    claimList.Add(new Claim(JwtClaimTypes.Name, opObj.data.name));
                    claimList.Add(new Claim(JwtClaimTypes.PhoneNumber, opObj.data.loginName));
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
                        opObj.msg
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
