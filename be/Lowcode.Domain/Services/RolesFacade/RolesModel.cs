using System;
using System.ComponentModel.DataAnnotations;

namespace Lowcode.Domain.Services.RolesFacade
{
    public class RolesModel
    {
        public Int32 Id { set; get; }

        [Required(ErrorMessage = "Tên vai trò yêu cầu nhập.")]
        public String RoleName { set; get; }

        [Required(ErrorMessage = "Mô tả yêu cầu nhập.")]
        public String Description { set; get; }

        public String Title { set; get; }
        public int? Level { get; set; }
        public String Permissions { set; get; }
    }
}