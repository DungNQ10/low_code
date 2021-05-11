using Lowcode.Domain.Services.RolesFacade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Lowcode.Domain.Data.Enums;
using IdentityModel;
using Lowcode.Framework.Utils;

namespace Lowcode.Api.Helpers
{
    public static class PrincipalExtensions
    {
        public static bool HasPermission(this ClaimsPrincipal claimsPrincipal, PermissionCode permissionCode)
        {
            var permissionsClaims = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "permissions");
            if (permissionsClaims == null) return false;

            return permissionsClaims.Value.Contains(";" + permissionCode.GetHashCode() + ";");
        }

        public static bool HasRole(this ClaimsPrincipal claimsPrincipal, RoleCode roleCode)
        {
            var permissionsClaims = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Role);
            if (permissionsClaims == null) return false;
            return permissionsClaims.Value.Contains(";" +roleCode.GetHashCode() + ";");
        }

        public static List<int> GetRoles(this ClaimsPrincipal claimsPrincipal)
        {
           
            var roleClaims = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Role);
            if (roleClaims == null) return new List<int>();
            return roleClaims.Value.ParseIds().ToList();
        }

        public static string GetPermissions(this ClaimsPrincipal claimsPrincipal)
        {
            var permissionsClaims = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "permissions");
            if (permissionsClaims == null) return string.Empty;

            return permissionsClaims.Value;
        }
        public static List<int> GetPermissionsList(this ClaimsPrincipal claimsPrincipal)
        {
            var permissionsClaims = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "permissions");
            if (permissionsClaims == null)
                return new List<int>();
            if(string.IsNullOrWhiteSpace(permissionsClaims.Value))
                  return new List<int>();
            return permissionsClaims.Value.ParseIds().ToList();
        }
    }
}
