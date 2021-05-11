using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lowcode.Domain.Data.Entity;

namespace Lowcode.Domain.Services.RolesFacade
{
    public interface IRolesService
    {
        Task<RolesModel> GetById(int rolesId);

        Task<ActionResult> Add(RolesModel roles);

        Task<ActionResult> Update(RolesModel roles);

        Task<ActionResult> Delete(int[] Ids);

        Task<List<RolesModel>> List(RoleFilterModel filter);

        List<RolesModel> List(Expression<Func<Roles, bool>> query);

        Task<bool> HasPermission(int[] roleIds, PermissionCode permission);

        Task<ActionResult> UpdateList(List<RolesModel> roles);
    }
}