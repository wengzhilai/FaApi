using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Idsvr4.IdentityServerExtensions
{
    /// <summary>
    /// 短信登录
    /// </summary>
    public class SMSGrantValidator : IExtensionGrantValidator
    {
        private readonly ISmsSendRepository smsSend;

        public string GrantType => "SMSGrantType";

        public SMSGrantValidator(ISmsSendRepository smsSend)
        {
            this.smsSend = smsSend;
        }

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var smsCode = context.Request.Raw.Get("smsCode");
            var phoneNumber = context.Request.Raw.Get("phoneNumber");

            if (string.IsNullOrEmpty(smsCode) || string.IsNullOrEmpty(phoneNumber))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
            }

            //var result = await smsSend.Count(phoneNumber, smsCode);
            ////表示验证码有效
            //if (result>0)
            //{
                List<Claim> claimList = new List<Claim>();
                claimList.Add(new Claim(JwtClaimTypes.Role, "superadmin"));
                context.Result = new GrantValidationResult(
                 subject: phoneNumber,
                 authenticationMethod: GrantType,
                 claims: claimList);
            //}
            //else
            //{
            //    context.Result = new GrantValidationResult(
            //        TokenRequestErrors.InvalidGrant,
            //        "短信码错误!"
            //        );
            //}
        }
    }
}
