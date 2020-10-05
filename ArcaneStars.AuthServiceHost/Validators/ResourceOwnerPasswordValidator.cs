using ArcaneStars.AuthServiceHost.Proxies;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ArcaneStars.AuthServiceHost.Validators
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserServiceProxy _userServiceProxy;
        public ResourceOwnerPasswordValidator(IUserServiceProxy userServiceProxy) 
        {
            _userServiceProxy = userServiceProxy;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var result = await _userServiceProxy.CheckUser(context.UserName, context.Password);

            if(result!=null)
                context.Result = new GrantValidationResult(
                                    subject: context.UserName,
                                    authenticationMethod: "custom",
                                    claims: new Claim[] {
                                        new Claim("user_id", result.Id.ToString()),
                                        new Claim("user_name", result.UserName),
                                        new Claim("mobile", result.Mobile),
                                        new Claim("email", result.Email)
                                    }
                );
            else
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid client credential");

            //return Task.CompletedTask;
        }
    }


    public class ProfileService : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var claims = context.Subject.Claims.ToList();
            context.IssuedClaims = claims.ToList();
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
        }
    }
}
