using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Lowcode.Api.Helpers
{
    public static class IdentityHelper
    {
        public static int GetUserId(this IIdentity identity)
        {
            if (identity == null) return -1;

            var auth = (ClaimsIdentity)identity;
            var claim = auth.Claims.SingleOrDefault(c => c.Type == "sub");
            var id = Convert.ToInt32(claim.Value);

            return id;
        }
    }
}