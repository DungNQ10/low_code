namespace Lowcode.Domain.Services.UserFacade
{
    /// <summary>
    /// Trạng thái tài khoản
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// Chưa kích hoạt
        /// </summary>
        NotActived = 1,

        /// <summary>
        /// Kích hoạt
        /// </summary>
        Actived = 2,

        /// <summary>
        /// Bị vô hiệu
        /// </summary>
        Disabled = 3
    }
}