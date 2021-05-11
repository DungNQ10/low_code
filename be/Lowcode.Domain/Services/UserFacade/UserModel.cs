using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using Lowcode.Domain.Shared;
using Lowcode.Framework.Formatter;

namespace Lowcode.Domain.Services.UserFacade
{
    /// <summary>
    /// Tài khoản của hệ thống
    /// </summary>
    public class UserModel : BaseModelAttachment
    {
        /// <summary>
        /// Mã tài khoản
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Họ tên tài khoản
        /// </summary>
        [MaxLength(250, ErrorMessage = "Họ tên tối đa là 250 ký tự")]
        [MinLength(5, ErrorMessage = "Họ tên tối thiểu là 5 ký tự")]
        public string FullName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Birthday { set; get; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Email đăng nhập
        /// </summary>
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Nullable<bool> Gender { get; set; }

        /// <summary>
        /// Số di động
        /// </summary>
       // [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        //[RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Mobile { get; set; }

        /// <summary>
        /// Trạng thái tài khoản
        /// </summary>
        [Required]
        public UserStatus Status { get; set; }

        /// <summary>
        /// Ảnh minh họa dạng base 64
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public System.DateTime CreatedDate { get; set; }

        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        /// <summary>
        /// Mã user tạo tài khoản
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Mã user cập nhật tài khoản
        /// </summary>
        public Nullable<int> UpdatedBy { get; set; }

        /// <summary>
        /// Vai trò của user
        /// </summary>
        public string UserRoles { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Mã đối tượng sử dụng tài khoản (Staff, Distributor)
        /// </summary>
        public AccountType? ObjectId { set; get; }

        /// <summary>
        /// Loại tài khoản
        /// </summary>
        public AccountType? AccountType { set; get; }

        /// <summary>
        /// Token xác thực tính hợp lệ của tài khoản
        /// </summary>
        public Guid UserModifiedTimeStamp { set; get; }

        /// <summary>
        /// Quyền của user đăng nhập
        /// </summary>
        public string Permissions { get; set; }

        public string RoleNames { get; set; }

        //public override bool Valid(ref string message)
        //{
        //    if (String.IsNullOrEmpty(this.Email) || String.IsNullOrEmpty(this.Mobile))
        //    {
        //        message = "Email, Mobile là bắt buộc";
        //        return false;
        //    }
        //    return base.Valid(ref message);
        //}
        public int? WorkingPackageId { get; set; }

        public string WhiteList { get; set; }
        public string WorkingPackageTime { get; set; }
    }
}