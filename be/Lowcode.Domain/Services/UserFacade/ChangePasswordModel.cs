using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Lowcode.Domain.Services.UserFacade
{
    public class ChangePasswordModel
    {
        [JsonIgnore]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Old Password is required")]
        [StringLength(255, ErrorMessage = "Old Password must be between 8 and 255 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Password must be between 8 and 255 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Password must be between 8 and 255 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}