namespace Lowcode.Domain.Services.RolesFacade
{
    /// <summary>
    /// Các vai trò hệ thống không được phép xóa
    /// </summary>
    public enum SystemRoles
    {
        /// <summary>
        /// Quản trị hệ thống
        /// </summary>
        Administrators = 1,

        /// <summary>
        /// Kế toán viên
        /// </summary>
        Accountants = 16,

        /// <summary>
        /// Kế toán trưởng
        /// </summary>
        ChiefAccountant = 17
    }
}