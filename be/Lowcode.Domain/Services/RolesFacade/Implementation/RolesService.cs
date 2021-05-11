using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lowcode.Domain.Data;
using Lowcode.Domain.Data.Entity;
using Lowcode.Framework.Data;
using Lowcode.Framework.Utils;

namespace Lowcode.Domain.Services.RolesFacade.Implementation
{
    public class RolesService : IRolesService
    {
        private IEntityRepository<Roles, DomainContext> _rolesRepository;

        public RolesService(IEntityRepository<Roles, DomainContext> rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task<ActionResult> Add(RolesModel roles)
        {
            //add to storage
            //var entity = roles.Map<RolesModel, Roles>();
            var entity = DomainMaps.Mapper.Map<Roles>(roles);
            var res = await _rolesRepository.AddAsync(entity);

            return new ActionResult()
            {
                Success = (res != null ? true : false)
            };
        }

        public async Task<ActionResult> Delete(int[] Ids)
        {
            var roleIds = Enum.GetValues(typeof(SystemRoles)).Cast<int>().ToArray();
            if (Ids.Any(c => roleIds.Contains(c)))
            {
                return new ActionResult()
                {
                    Success = false,
                    Message = "Không được xóa các role của hệ thống"
                };
            }

            var res = await _rolesRepository.DeleteManyAsync(c => Ids.Contains(c.Id));

            return new ActionResult()
            {
                Success = (res > 0 ? true : false)
            };
        }

        public async Task<RolesModel> GetById(int rolesId)
        {
            var entity = await _rolesRepository.GetByIdAsync(rolesId);
            return entity.Map<RolesModel>();
        }

        public async Task<List<RolesModel>> List(RoleFilterModel filter)
        {
            var res = await _rolesRepository.FetchAsync(c => (!String.IsNullOrEmpty(filter.Search) ? true : true), c => c.Level, filter.Paging);

            return res.Map<List<RolesModel>>();
        }

        public List<RolesModel> List(Expression<Func<Roles, bool>> query)
        {
            var res = _rolesRepository.Fetch(query);
            return res.Map<List<RolesModel>>();
        }

        public async Task<ActionResult> Update(RolesModel roles)
        {
            if (roles.Id != 0)
            {
                var exist = await _rolesRepository.GetByIdAsync(roles.Id);
                if (exist == null)
                    return new ActionResult()
                    {
                        Success = false,
                        Message = "roles không tồn tại"
                    };
            }

            //update to storage
            var entity = roles.Map<Roles>();
            var res = await _rolesRepository.UpdateAsync(entity);

            return new ActionResult()
            {
                Success = res
            };
        }

        public async Task<ActionResult> UpdateList(List<RolesModel> roles)
        {
            //update to storage
            foreach (var item in roles)
            {
                try
                {
                    var ids = item.Permissions.ParseIds().Distinct().ToList();
                    item.Permissions = ";" + string.Join(";", ids) + ";";
                }
                catch (Exception ex)
                {
                }
            }
            var entities = roles.CloneToListModels<RolesModel, Roles>();
            var res = await _rolesRepository.UpdateManyAsync(entities);

            return new ActionResult()
            {
                Success = res
            };
        }

        public async Task<bool> HasPermission(int[] roleIds, PermissionCode permission)
        {
            var code = $";{permission.GetHashCode()};";
            var roles = await _rolesRepository.FetchAsync(c => roleIds.Contains(c.Id));
            if (roles.Any(c => !String.IsNullOrEmpty(c.Permissions) && c.Permissions.Contains(code)))
            {
                return true;
            }
            return false;
        }

        public static List<PermissionGroup> ListPermissions()
        {
            return PermissionHelper.GetPermissionGroups();
        }
    }
}