namespace Lowcode.Domain.Data.Entity
{
    /// <summary>
    /// Vai trò của hệ thống
    /// </summary>
    public partial class Roles
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int? Level { get; set; }
        public string Permissions { get; set; }
    }
}