/// <summary>
/// Tài khoản của nhân viên trong hệ thống
/// </summary>

namespace Lowcode.Domain.Data.Entity
{
    using System;
    using Lowcode.Domain.Services.UserFacade;

    public partial class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime? Birthday { set; get; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string Mobile { get; set; }
        public UserStatus Status { get; set; }
        public string Avatar { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UserRoles { get; set; }
        public string Address { get; set; }
        public AccountType? ObjectId { set; get; }
        public AccountType? AccountType { set; get; }

        public Guid UserModifiedTimeStamp { set; get; }
        public int? WorkingPackageId { get; set; }

        /// <summary>
        /// danh sach ip duoc access
        /// </summary>
        public string WhiteList { get; set; }
    }
}