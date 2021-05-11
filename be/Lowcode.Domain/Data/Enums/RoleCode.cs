using System.ComponentModel;

namespace Lowcode.Domain.Data.Enums
{
    public enum RoleCode
    {
        [Description("Ban giám đốc")]
        BOD = 2,

        [Description("Quản lý dự án")]
        PM = 3,

        [Description("Giám đốc")]
        Director = 5,

        [Description("Nhóm phó")]
        NhomPho = 9,

        [Description("Nhóm trưởng")]
        NhomTruong = 10,

        [Description("Nhân viên chính thức")]
        NhanVienChinhThuc = 11,

        [Description("Chuyên gia nước ngoài")]
        ChuyenGiaNuocNgoai = 13,

        [Description("Thực tập sinh nước ngoài")]
        ThuctapsinhNuocngoai = 14,

        [Description("Cộng tác viên chính thức")]
        CongTacVienChinhthuc = 15,

        [Description("Cộng tác viên phụ")]
        CongTacVienPhu = 16,

        [Description("CEO")]
        CEO = 17,

        [Description("PRINCIPAL")]
        PRINCIPAL = 26
    }
}