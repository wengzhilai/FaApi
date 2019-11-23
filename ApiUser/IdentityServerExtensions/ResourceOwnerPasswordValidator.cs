using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiUser.IdentityServerExtensions
{
    /// <summary>
    ///ResourceOwnerPassword模式下用于自定义登录验证 
    /// </summary>
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IConfiguration config;

        public ResourceOwnerPasswordValidator(IConfiguration config)
        {
            this.config = config;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            string verificationCode = context.Request.Raw.Get("VerificationCode");

            //根据context.UserName和context.Password与数据库的数据做校验，判断是否合法
            if (context.UserName == "test" && context.Password == "test")
            {
                List<Claim> claimList = new List<Claim>();
                claimList.Add(new Claim("userID", "1"));

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
                    "invalid custom credential"
                    );
            }
            return Task.FromResult(0);
        }
    }
}
