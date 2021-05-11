using System.Collections.Generic;

namespace Lowcode.Domain.Services.UserFacade
{
    public class UserFormIPModel
    {
        public List<int> UserIds { get; set; }
        public string IpAddress { get; set; }
    }
}