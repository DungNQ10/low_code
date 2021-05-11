// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Lowcode.Authentication.Configuration;
using Lowcode.Authentication.Helpers;

using Lowcode.Domain.Services.UserFacade;
using Lowcode.Domain.Services.RolesFacade;

namespace Lowcode.Authentication.IdentityServer
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        // scopes define the API resources in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("wgd_api", "WGD PMS API")
                {
                    //Scopes =
                    //{
                    //    new Scope("zb_api", "PCC1 PMS API"),
                    //    new Scope(IdentityServerConstants.StandardScopes.OpenId),
                    //    new Scope(IdentityServerConstants.StandardScopes.Profile),
                    //    new Scope(IdentityServerConstants.StandardScopes.OfflineAccess)
                    //}
                }
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients(AppSettings appSettings)
        {
            var uris = appSettings.ClientAppRedirectUri;

            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("m9wfTF5ZXCvL9u5AOIXBK1zWe9Kd9Kzb".Sha256())
                    },
                    AllowedScopes = { "wgd_api" }
                },

                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    RedirectUris = new List<string>(uris),
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {
                        "wgd_api",
                        "openid",
                        "profile",
                        "offline_access",
                    },
                    AllowOfflineAccess = true,
                    Enabled = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedCorsOrigins = new  List<string>(uris),
                    AccessTokenType = AccessTokenType.Jwt,
                    IdentityTokenLifetime = 3000,
                    AccessTokenLifetime = 3600*24*30,
                    AuthorizationCodeLifetime = 300
                }
            };
        }

        //build claims array from user data
        public static Claim[] GetUserClaims(UserModel user, IRolesService _roleService)
        {
            string result = string.Empty;

            if (!String.IsNullOrEmpty(user.UserRoles))
            {
                var ids = user.UserRoles.ParseIds();
                var roles = _roleService.List(c => ids.Contains(c.Id));
                var permissions = new List<int>();
                foreach (var role in roles)
                {
                    if (!String.IsNullOrEmpty(role.Permissions))
                    {
                        ids = role.Permissions.ParseIds();
                        permissions.AddRange(ids);
                    }
                }
                var res = permissions.ToArray().RemoveDuplicates();
                result = string.Join(";", res);
                if (!String.IsNullOrEmpty(result))
                    result = ";" + result + ";";
            }

            return new Claim[]
            {
                new Claim("user_id", user.Id.ToString() ?? ""),
                new Claim(JwtClaimTypes.Name, user.FullName ?? string.Empty),
                new Claim(JwtClaimTypes.Email, user.Email  ?? ""),
                new Claim("timestemp",user.UserModifiedTimeStamp.ToString()),
                ////roles
                new Claim(JwtClaimTypes.Role, user.UserRoles),
                new Claim("permissions", result),
            };
        }
    }
}