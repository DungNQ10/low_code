using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using Lowcode.Domain.Services.UserFacade;
using Lowcode.Domain.Services.RolesFacade;
using Lowcode.Domain.Services.RolesFacade.Implementation;
using Lowcode.Api.Helpers;
using Lowcode.Domain.Data.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lowcode.Api.Modules.Users
{
    public class RoleController : Controller
    {
        private readonly IRolesService _rolesService;
        private readonly IUserService userService;
        public RoleController(IRolesService rolesService, IUserService userService)
        {
            _rolesService = rolesService;
            this.userService = userService;
        }

        /// <summary>
        /// Filter roles
        /// </summary>
        /// <returns></returns>
        [HttpPost("v1/roles/list")]
        //[Authorize(Policy = nameof(PermissionCode.MANAGE_ROLES))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Unauthorized, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.InternalServerError, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.BadRequest, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Forbidden, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.NotFound)]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<List<RolesModel>>))]
        public async Task<IActionResult> FilterRoles([FromBody]Lowcode.Domain.Services.RolesFacade.RoleFilterModel filter)
        {
            var res = await _rolesService.List(filter);

            return Json(new ResultBase<List<RolesModel>>() { success = true, data = res });
        }

        /// <summary>
        /// Filter roles
        /// </summary>
        /// <returns></returns>
        [HttpPost("v1/roles/listAdmin")]
        [Authorize(Policy = nameof(PermissionCode.MANAGE_ROLES))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Unauthorized, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.InternalServerError, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.BadRequest, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Forbidden, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.NotFound)]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<List<RolesModel>>))]
        public async Task<IActionResult> FilterRolesAdmin([FromBody]Lowcode.Domain.Services.RolesFacade.RoleFilterModel filter)
        {
            var res = await _rolesService.List(filter);

            return Json(new ResultBase<List<RolesModel>>() { success = true, data = res });
        }

        /// <summary>
        /// Get role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize(Policy = nameof(PermissionCode.MANAGE_ROLES))]
        [Route("v1/roles/get_role")]
        [HttpGet]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<List<RolesModel>>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<RolesModel>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Unauthorized, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.InternalServerError, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.BadRequest, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Forbidden, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetRole(int id)
        {
            var res = await _rolesService.GetById(id);

            return Json(new ResultBase<RolesModel>() { success = true, data = res });
        }

        /// <summary>
        /// Update roles
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [Authorize(Policy = nameof(PermissionCode.MANAGE_ROLES))]
        [Route("v1/roles/update")]
        [HttpPut]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<RolesModel>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Unauthorized, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.InternalServerError, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.BadRequest, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Forbidden, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.NotFound)]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<bool>))]
        public async Task<ActionResult> UpdateAsync([FromBody]RolesModel role)
        {
            if (!ModelState.IsValid)
                return Json(new ResultBase<ActionResult>() { success = false, message = ModelState.GetErrorsMessage() });

            // role.Title = role.Description;
            if (role.Id == 0)
            {
                var res = await _rolesService.Add(role);

                return Json(new ResultBase<RolesModel>() { success = res.Success, message = res.Message });
            }
            else
            {
                var res = await _rolesService.Update(role);

                return Json(new ResultBase<RolesModel>() { success = res.Success, message = res.Message });
            }

        }

        [Route("v1/roles/updatelist")]
        [HttpPost]
        public async Task<ActionResult> UpdateListAsync([FromBody]List<RolesModel> roles)
        {

            var res = await _rolesService.UpdateList(roles);
            if (res.Success)
                userService.UpdateUserTimeStemp(roles.Select(c => c.Id).ToList());
            return Json(new ResultBase<RolesModel>() { success = res.Success, message = res.Message });

        }

        /// <summary>
        /// Get permissions
        /// </summary>
        /// <returns></returns>
        //[Authorize(Policy = nameof(PermissionCode.MANAGE_ROLES))]
        [Route("v1/roles/list_permissions")]
        [HttpGet]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<RolesModel>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Unauthorized, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.InternalServerError, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.BadRequest, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.Forbidden, Type = typeof(ResultBase<>))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.NotFound)]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<List<PermissionGroup>>))]
        [Authorize(Policy = nameof(PermissionCode.MANAGE_PERMISSION))]
        public ActionResult ListPermissions()
        {
            var res = RolesService.ListPermissions();

            return Json(new ResultBase<List<PermissionGroup>>() { success = true, data = res });
        }

        /// <summary>
        /// Delete
        /// </summary>
        [HttpDelete]
        [Authorize(Policy = nameof(PermissionCode.MANAGE_ROLES))]
        [Route("v1/roles/delete")]
        public async Task<IActionResult> Delete([FromBody]int id)
        {
            var notdeleteRoles = new List<int>()
            {
               (int) RoleCode.BOD,
               (int) RoleCode.CEO,
               (int) RoleCode.ChuyenGiaNuocNgoai,
               (int) RoleCode.NhanVienChinhThuc,
               (int) RoleCode.CongTacVienChinhthuc,
               (int) RoleCode.CongTacVienPhu,
               (int) RoleCode.Director,
               (int) RoleCode.NhomPho,
               (int) RoleCode.NhomTruong,
               (int) RoleCode.PM,
               (int) RoleCode.PRINCIPAL,
               (int) RoleCode.ThuctapsinhNuocngoai
            };
            if (notdeleteRoles.Contains(id))
                return Json(new ResultBase<bool>() { success = false, message = "Không thể xóa role của hệ thống." });
            var res = await _rolesService.Delete(new int[] { id });
            return Json(new ResultBase<bool>() { success = res.Success, data = true, message = res.Message });
        }
    }
}
