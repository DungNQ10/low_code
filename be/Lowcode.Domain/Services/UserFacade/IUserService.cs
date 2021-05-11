using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lowcode.Domain.Data.Entity;
using Lowcode.Domain.Services.RolesFacade;

namespace Lowcode.Domain.Services.UserFacade
{
    public interface IUserService
    {
        Task<UserModel> GetByIdAsync(int userId);

        UserModel GetById(int userId);

        ActionResultType<UserModel> Add(UserModel user);

        ActionResult Update(UserModel user);

        Task<ActionResult> Delete(int[] Ids);

        Task<ActionResult> DeleteManyAsync(Expression<Func<User, bool>> expression);

        Task<List<UserModel>> List(UserFilterModel filter);

        Task<UserModel> SingleAsync(Expression<Func<User, bool>> query);

        /// <summary>
        /// Reset mật khẩu theo user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<ActionResult> ResetPassword(int userId, string newPassword);

        /// <summary>
        /// Cập nhật tài khoản co thay đổi time stamp
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="forceChangeTimestamp"></param>
        /// <returns></returns>
        ActionResult Update(UserModel staff, bool forceChangeTimestamp);

        /// <summary>
        /// Khóa hoặc mở khóa tài khoản
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        Task<ActionResult> ToggleLock(int staffId);

        /// <summary>
        /// Đổi mật khẩu user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<ActionResult> ChangePassword(int userId, string oldPassword, string newPassword);

        /// <summary>
        /// Change current user password
        /// </summary>
        /// <param name="changePassword"></param>
        /// <returns></returns>
        Task<ActionResult> ChangePassword(ChangePasswordModel changePassword);

        /// <summary>
        /// Xác thực thông tin user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="encryptedPassword"></param>
        /// <returns></returns>
        ActionResultType<UserModel> VerifyPassword(string userName, string encryptedPassword);

        /// <summary>
        /// get all users
        /// </summary>
        /// <returns></returns>
        Task<List<UserModel>> ListAll(UserStatus? status);

        List<int> GetRoles(int userId);

        /// <summary>
        /// check user has been modified?
        /// </summary>
        /// <param name="timeStemp"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool CheckModified(string timeStemp, int userId);

        void UpdateUserTimeStemp(List<int> roles);

        ActionResult SaveIPs(UserFormIPModel model);

        /// <summary>
        /// get user by permission
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        List<UserModel> GetUserByPermission(PermissionCode permission);

        /// <summary>
        /// get users by permissions
        /// </summary>
        /// <param name="permissions"></param>
        /// <returns></returns>
        List<UserModel> GetUserByPermission(List<PermissionCode> permissions);
    }
}