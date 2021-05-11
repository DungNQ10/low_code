using System.Collections.Generic;

namespace Lowcode.Domain.Services.RolesFacade
{
    public class Permission
    {
        public PermissionCode Code { set; get; }

        public string Name { set; get; }

        public string HASH_CODE
        {
            get
            {
                return Code.ToString();
            }
        }

        public int GroupId { set; get; }
    }

    public class PermissionGroup
    {
        public int GroupId { set; get; }
        public string Name { set; get; }
        public List<Permission> Permissions { set; get; }
    }
}