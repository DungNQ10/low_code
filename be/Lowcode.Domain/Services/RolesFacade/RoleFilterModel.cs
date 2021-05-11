using Lowcode.Framework.Data;

namespace Lowcode.Domain.Services.RolesFacade
{
    public class RoleFilterModel
    {
        public string Search { set; get; }

        public PagingInfo Paging { set; get; }
    }
}