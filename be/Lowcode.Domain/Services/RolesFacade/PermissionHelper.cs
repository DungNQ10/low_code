using System;
using System.Collections.Generic;
using System.Linq;

namespace Lowcode.Domain.Services.RolesFacade
{
    public static class PermissionHelper
    {
        public static List<PermissionGroup> GetPermissionGroups()
        {
            var groups = new List<PermissionGroup>();
            var groupCodes = Enum.GetValues(typeof(PermissionGroupCode)).Cast<PermissionGroupCode>().ToList();
            foreach (var groupCode in groupCodes)
            {
                groups.Add(new PermissionGroup()
                {
                    GroupId = (int)groupCode,
                    Name = groupCode.ToString(),
                });
            }

            var permissions = new List<Permission>();
            foreach (var p in Enum.GetValues(typeof(PermissionCode)).Cast<PermissionCode>())
            {
                var attr = p.GetAttributeOfType<PermissionDetailsAttribute>();
                permissions.Add(new Permission
                {
                    Code = p,
                    Name = attr.Name,
                    GroupId = (int)attr.Group
                });
            }

            //auto build permission properties for group
            foreach (var group in groups)
            {
                var groupPermissions = permissions.Where(c => c.GroupId == group.GroupId);
                group.Permissions = new List<Permission>();
                group.Permissions.AddRange(groupPermissions);
            }

            return groups;
        }
    }
}