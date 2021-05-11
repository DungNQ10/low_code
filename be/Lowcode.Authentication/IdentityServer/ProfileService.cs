using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lowcode.Domain.Services.UserFacade;
using Microsoft.AspNetCore.Authorization;
using Lowcode.Domain.Services.RolesFacade;

namespace Lowcode.Authentication.IdentityServer
{
    public class ProfileService : IProfileService
    {
        //services
        private readonly IUserService _authorizationService;
        private readonly IRolesService _rolesService;

        public ProfileService(IUserService authorizationService, IRolesService rolesService)
        {
            _authorizationService = authorizationService;
            _rolesService = rolesService;
        }

        //Get user profile date in terms of claims when calling /connect/userinfo
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                //depending on the scope accessing the user data.
                if (!string.IsNullOrEmpty(context.Subject.Identity.Name))
                {
                    //get user from db (in my case this is by email)
                    var user = await _authorizationService.GetByIdAsync(Convert.ToInt32(context.Subject.Identity.Name));

                    if (user != null)
                    {
                        var claims = Config.GetUserClaims(user, _rolesService);

                        //set issued claims to return
                        context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                    }
                }
                else
                {
                    //get subject from context (this was set ResourceOwnerPasswordValidator.ValidateAsync),
                    //where and subject was set to my user id.
                    var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");

                    if (!string.IsNullOrEmpty(userId?.Value) && long.Parse(userId.Value) > 0)
                    {
                        //get user from db (find user by user id)
                        var user = await _authorizationService.GetByIdAsync(int.Parse(userId.Value));

                        // issue the claims for the user
                        if (user != null)
                        {
                            var claims = Config.GetUserClaims(user, _rolesService);

                            context.IssuedClaims = claims.ToList();//.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //log your error
            }
        }

        //check if user account is active.
        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                //get subject from context (set in ResourceOwnerPasswordValidator.ValidateAsync),
                var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "user_id");

                if (!string.IsNullOrEmpty(userId?.Value) && int.Parse(userId.Value) > 0)
                {
                    var user = await _authorizationService.GetByIdAsync(int.Parse(userId.Value));

                    if (user != null)
                    {
                        context.IsActive = user.Status == UserStatus.Actived ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error logging
            }
        }
    }
}
