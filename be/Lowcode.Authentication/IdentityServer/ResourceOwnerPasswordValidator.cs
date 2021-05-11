using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Lowcode.Domain.Services.UserFacade;
using Lowcode.Domain.Services.RolesFacade;
using Lowcode.Framework.Utils;

namespace Lowcode.Authentication.IdentityServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        //repository to get user from db
        private readonly IUserService _authorizationService;
        private readonly IRolesService _rolesService;

        public ResourceOwnerPasswordValidator(IUserService authorizationService, IRolesService rolesService)
        {
            _authorizationService = authorizationService; //DI
            _rolesService = rolesService;
        }

        //this is used to validate your user account with provided grant at /connect/token
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                //get your user model from db (by username - in my case its email)
                //var user = await _authorizationService.SingleAsync(c => c.Email == context.UserName || c.Mobile == context.UserName);
                var result = _authorizationService.VerifyPassword(context.UserName, EncryptUtil.EncryptMD5(context.Password));

                //check if password match - remember to hash password if stored as hash in db
                if (result.Success && result.Data != null)
                {
                    var userModel = result.Data;
                    context.Result = new GrantValidationResult(
                       subject: userModel.Id.ToString(),
                       authenticationMethod: "custom",
                       claims: Config.GetUserClaims(userModel, _rolesService));
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid user or password");
                }
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, ex.ToString());
            }
        }
    }
}
