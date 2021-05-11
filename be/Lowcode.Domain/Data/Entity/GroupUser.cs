using System;

namespace Lowcode.Domain.Data.Entity
{
    public class GroupUser
    {
        public Int32 Id { get; set; }

        public Int32 GroupId { get; set; }

        public Int32 UserId { get; set; }

        public Boolean IsLeader { get; set; }
    }
}