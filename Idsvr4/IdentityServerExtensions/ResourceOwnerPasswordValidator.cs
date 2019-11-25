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

            var opObj = await login.UserLogin(new Models.LogingDto { LoginName = context.UserName, Password = context.Password });

            if (opObj.IsSuccess && opObj.Data!=null)
            {

                List<Claim> claimList = new List<Claim>();

                claimList.Add(new Claim(JwtClaimTypes.Id, opObj.Data.ID.ToString()));
                if(opObj.Data.roleIdList!=null) claimList.Add(new Claim(JwtClaimTypes.Role, string.Join(",", opObj.Data.roleIdList)));
                claimList.Add(new Claim(JwtClaimTypes.Name, opObj.Data.NAME));
                claimList.Add(new Claim(JwtClaimTypes.PhoneNumber, opObj.Data.LOGIN_NAME));
                
                context.Result = new GrantValidationResult(
                 subject: context.UserName,
                 authenticationMethod: OidcConstants.AuthenticationMethods.Password,
                 claims: claimList);
            }
            else
            {
                //验证失败
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    opObj.Msg
                    );
            }
        }
    }
}
