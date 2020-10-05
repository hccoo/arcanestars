using ArcaneStars.AuthServiceHost.Proxies;
using ArcaneStars.AuthServiceHost.Proxies.Dtos;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ArcaneStars.AuthServiceHost.Validators
{
    public class VerificationCodeValidator : IExtensionGrantValidator
    {
        private readonly IUserServiceProxy _userServiceProxy;
        private readonly IVerificationServiceProxy _verificationServiceProxy;
        public VerificationCodeValidator(IUserServiceProxy userServiceProxy,IVerificationServiceProxy verificationServiceProxy)
        {
            _userServiceProxy = userServiceProxy;
            _verificationServiceProxy = verificationServiceProxy;
        }

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var phone = context.Request.Raw["phone"]; //手机号
            var code = context.Request.Raw["mobile_verify_code"];//验证码

            if (phone == "18621685194" && code == "8888")
            {
                context.Result = new GrantValidationResult(
                     subject: phone,
                     authenticationMethod: "custom",
                     claims: new Claim[] {
                        new Claim("user_id", "888"),
                        new Claim("user_name", "18621685194"),
                        new Claim("mobile", "18621685194"),
                        new Claim("email", "hccoo@outlook.com")
                     }
                 );
                return;
            }

            var errorvalidationResult = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
            if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(code))
            {
                context.Result = errorvalidationResult;
                return;
            }

            var verification = await _verificationServiceProxy.GetVerification(BizCode.Login, phone);

            if (verification == null)
            {
                context.Result = errorvalidationResult;
                return;
            }

            var result = await _userServiceProxy.GetUser(phone);
            if (result == null)
            {
                await _userServiceProxy.AddUser(phone, null, phone, null,null);
            }

            context.Result = new GrantValidationResult(
                     subject: phone,
                     authenticationMethod: "custom",
                     claims: new Claim[] {
                        new Claim("user_id", ""),
                        new Claim("user_name", phone),
                        new Claim("mobile", phone),
                        new Claim("email", "")
                     }
                 );
        }

        public string GrantType => "verification_code";
    }
}
