using System.Collections.Generic;
using Lowcode.Framework.Data;

namespace Lowcode.Domain.Services.UserFacade
{
    public class UserFilterModel
    {
        public string Search { set; get; }

        public AccountType? AccountType { set; get; }

        public UserStatus? UserStatus { set; get; }

        public int? CreatorId { set; get; }

        public int? UpdaterId { set; get; }
        public List<int> SelectedRoleKeys { get; set; }

        public PagingInfo Paging { set; get; }
    }
}