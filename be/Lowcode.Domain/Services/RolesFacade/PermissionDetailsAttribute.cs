using System;

namespace Lowcode.Domain.Services.RolesFacade
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class PermissionDetailsAttribute : Attribute
    {
        private readonly PermissionGroupCode group;
        private readonly string name;

        public PermissionDetailsAttribute(PermissionGroupCode group, string name)
        {
            this.group = group;
            this.name = name;
        }

        public virtual string Name
        {
            get { return name; }
        }

        public virtual PermissionGroupCode Group
        {
            get { return group; }
        }
    }
}