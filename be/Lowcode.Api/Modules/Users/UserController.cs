using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Lowcode.Api.Helpers;
using Lowcode.Domain.Services.RolesFacade;
using Lowcode.Domain.Services.UserFacade;
using UserFilterModel = Lowcode.Domain.Services.UserFacade.UserFilterModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lowcode.Api.Modules.Users
{
    /// <summary>
    /// User api to access user's profile and manage user's account
    /// </summary>

    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRolesService _rolesService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="rolesService"></param>
        public UserController(IUserService userService, IRolesService rolesService)
        {
            _userService = userService;
            _rolesService = rolesService;
        }

        /// <summary>
        /// Get user current logged user info
        /// </summary>
        /// <returns></returns>
        [HttpGet("v1/user/get_my_profile")]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultBase<UserModel>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<RolesModel>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Unauthorized, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.InternalServerError, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.BadRequest, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Forbidden, Type = typeof(ResultBase<>))]
        public ActionResult GetMyProfile()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var userModel = _userService.GetById(userId);
            var roleIds = userModel.UserRoles.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(c => Convert.ToInt32(c)).ToList();
            var roles = _rolesService.List(c => roleIds.Contains(c.Id));
            if (roles.Count > 0)
            {
                userModel.RoleNames = string.Join(", ", roles.Select(c => c.RoleName).ToList());
            }

            var permissions = HttpContext.User.GetPermissions();
            userModel.Permissions = permissions;

            //log time start work.

            return Json(new ResultBase<UserModel>() { success = true, data = userModel });
        }

        /// <summary>
        /// Trả về role user đăng nhập
        /// </summary>
        /// <returns></returns>
        [HttpGet("v1/user/get_my_roles")]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultBase<List<RolesModel>>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<RolesModel>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Unauthorized, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.InternalServerError, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.BadRequest, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Forbidden, Type = typeof(ResultBase<>))]
        public ActionResult GetMyRoles()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var user = _userService.GetById(userId);

            var roleIds = new List<int>();
            var roles = new List<RolesModel>();

            if (!String.IsNullOrEmpty(user.UserRoles))
            {
                roleIds = user.UserRoles.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => Convert.ToInt32(c)).ToList();
                roles = _rolesService.List(c => roleIds.Contains(c.Id));
            }

            return Json(new ResultBase<List<RolesModel>>() { success = true, data = roles });
        }

        /// <summary>
        /// Get user info by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("v1/user/get_user_info")]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultBase<UserModel>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<RolesModel>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Unauthorized, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.InternalServerError, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.BadRequest, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Forbidden, Type = typeof(ResultBase<>))]
        public ActionResult GetUserInfo(int userId)
        {
            var userModel = _userService.GetById(userId);
            var permissions = HttpContext.User.GetPermissions();
            userModel.Permissions = permissions;

            return Json(new ResultBase<UserModel>() { success = true, data = userModel });
        }

        /// <summary>
        /// Search users
        /// </summary>
        /// <param name="userSearchModel"></param>
        /// <returns></returns>
        [HttpPost("v1/user/search")]
        [Authorize(Policy = nameof(PermissionCode.MANAGE_USERS))]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultBase<PagedList<UserModel>>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<RolesModel>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Unauthorized, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.InternalServerError, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.BadRequest, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Forbidden, Type = typeof(ResultBase<>))]
        public async Task<IActionResult> Search([FromBody]UserFilterModel userSearchModel)
        {
            var result = await _userService.List(userSearchModel);

            return Json(new ResultBase<PagedList<UserModel>>() { success = true, data = new PagedList<UserModel>() { list = result, pager = userSearchModel.Paging } });
        }

        /// <summary>
        /// Add user (Admin)
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost("v1/user/add_user")]
        [Authorize(Policy = nameof(PermissionCode.MANAGE_USERS))]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultBase<UserModel>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<RolesModel>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Unauthorized, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.InternalServerError, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.BadRequest, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Forbidden, Type = typeof(ResultBase<>))]
        public ActionResult CreateUser([FromBody]UserModel userModel)
        {
            if (!ModelState.IsValid)
                return Json(new ResultBase<List<UserModel>>() { success = false, message = ModelState.GetErrorsMessage() });

            var result = _userService.Add(userModel);

            return Json(new ResultBase<UserModel>() { success = result.Success, data = result.Data, message = result.Message });
        }

        /// <summary>
        /// Update user (Admin)
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost("v1/user/update_user")]
        [Authorize()]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultBase<UserModel>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<RolesModel>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Unauthorized, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.InternalServerError, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.BadRequest, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Forbidden, Type = typeof(ResultBase<>))]
        public ActionResult UpdateUser([FromBody]UserModel userModel)
        {
            if (!ModelState.IsValid)
                return Json(new ResultBase<List<UserModel>>() { success = false, message = ModelState.GetErrorsMessage() });

            var result = _userService.Update(userModel);

            return Json(new ResultBase<UserModel>() { success = result.Success, message = result.Message });
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="changePasswordModel"></param>
        /// <returns></returns>
        [HttpPost("v1/user/change_password")]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultBase<bool>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<RolesModel>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Unauthorized, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.InternalServerError, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.BadRequest, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Forbidden, Type = typeof(ResultBase<>))]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordModel changePasswordModel)
        {
            if (!ModelState.IsValid) return Json(new ResultBase<List<UserModel>>() { success = false, message = ModelState.GetErrorsMessage() });

            var userId = HttpContext.User.Identity.GetUserId();

            changePasswordModel.UserId = userId;

            var result = await _userService.ChangePassword(changePasswordModel);

            return Json(new ResultBase<bool>() { success = result.Success, message = result.Message });
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [HttpPost("v1/user/reset_password")]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultBase<bool>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<RolesModel>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Unauthorized, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.InternalServerError, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.BadRequest, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Forbidden, Type = typeof(ResultBase<>))]
        public async Task<IActionResult> ResetPassword(int userId, string newPassword)
        {
            if (!ModelState.IsValid) return Json(new ResultBase<List<UserModel>>() { success = false, message = ModelState.GetErrorsMessage() });

            var result = await _userService.ResetPassword(userId, newPassword);

            return Json(new ResultBase<bool>() { success = result.Success, message = result.Message });
        }

        [Authorize(Policy = nameof(PermissionCode.MANAGE_USERS))]
        [HttpPost("v1/user/remove_user")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<bool>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<RolesModel>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Unauthorized, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.InternalServerError, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.BadRequest, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Forbidden, Type = typeof(ResultBase<>))]
        public async Task<IActionResult> RemoveUser([FromBody] int[] userIds)
        {
            if (!ModelState.IsValid) return Json(new ResultBase<List<UserModel>>() { success = false, message = ModelState.GetErrorsMessage() });

            var currentUser = HttpContext.User.Identity.GetUserId();

            var result = await _userService.Delete(userIds);

            return Json(new ResultBase<bool>() { success = result.Success, message = result.Message });
        }

        //[Authorize(Policy = nameof(PermissionCode.MANAGE_USERS))]
        [HttpGet("v1/user/listAll")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<UserModel>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Unauthorized, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.InternalServerError, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.BadRequest, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Forbidden, Type = typeof(ResultBase<>))]
        public async Task<IActionResult> ListAll(UserStatus? status)
        {
            if (!ModelState.IsValid) return Json(new ResultBase<List<UserModel>>() { success = false, message = ModelState.GetErrorsMessage() });

            var result = await _userService.ListAll(status);

            return Json(new ResultBase<List<UserModel>>() { success = true, data = result });
        }

        [SwaggerResponse((int)System.Net.HttpStatusCode.Forbidden, Type = typeof(ResultBase<bool>))]
        [HttpPost("v1/user/SaveIPs")]
        [Authorize(Policy = nameof(PermissionCode.MANAGE_USERS))]
        public IActionResult SaveIPs([FromBody] UserFormIPModel model)
        {
            var res = _userService.SaveIPs(model);
            return Json(res);
        }
    }
}