using System.Security.Principal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ZB.MoneyTransfer.Domain.Services.Users;

namespace DPVG.CRMV2.WebApi.Authorization
{
    /// <summary>
    /// User da chung thuc
    /// </summary>
    public static class AuthenticatedUser
    {
        /// <summary>
        /// Lấy userId của user đang đăng nhập
        /// </summary>
        /// <returns></returns>
        public static int GetUserId()
        {
            IIdentity identity = HttpContext.Current.User.Identity;
            if (identity == null) return -1;

            var auth = (ClaimsIdentity)identity;
            var claim = auth.Claims.SingleOrDefault(c => c.Type == "sub");
            var id = Convert.ToInt32(claim.Value);

            return id;
        }

        /// <summary>
        /// Lấy thông tin user đang đăng nhập
        /// </summary>
        public static UserModel CurrentUser
        {
            get
            {
                var userId = GetUserId();
                var user = (new UserService()).GetById(userId);
                return user;
            }
        }
    }
}