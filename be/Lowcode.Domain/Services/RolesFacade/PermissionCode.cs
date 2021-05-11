using System.ComponentModel;

namespace Lowcode.Domain.Services.RolesFacade
{
    public enum PermissionGroupCode
    {
        //SETTING = 1,
        CATEGORIES = 2,

        PROJECT = 3,
        //REPORT = 4,
        SYSTEM = 5,
        CONTRACT = 6,
        CONTRACT_TEMPLATE = 7,
        LIBRARY = 8,
        QUOTE = 9        
    }

    public enum PermissionCode
    {
        [PermissionDetails(PermissionGroupCode.SYSTEM, "Thêm tài khoản mới")]
        CREATE_ACCOUNTS_CATEGORIES = 11,

        [PermissionDetails(PermissionGroupCode.SYSTEM, "Cập nhật tài khoản")]
        UPDATE_ACCOUNTS_CATEGORIES = 12,

        [PermissionDetails(PermissionGroupCode.SYSTEM, "Xóa tài khoản")]
        DELETE_ACCOUNTS_CATEGORIES = 13,

        [PermissionDetails(PermissionGroupCode.SYSTEM, "Xem d/s tài khoản")]
        VIEW_ACCOUNTS_CATEGORIES = 14,

        [PermissionDetails(PermissionGroupCode.SYSTEM, "Quản lý người dùng")]
        MANAGE_USERS = 42,

        [PermissionDetails(PermissionGroupCode.SYSTEM, "Quản lý vai trò")]
        MANAGE_ROLES = 43,

        [PermissionDetails(PermissionGroupCode.SYSTEM, "Phân quyền")]
        MANAGE_PERMISSION = 44,

        [PermissionDetails(PermissionGroupCode.CATEGORIES, "Quản lý khách hàng")]
        [Description("Cập nhật khách hàng")]
        KHACHHANG_QUANLY = 100,

        [PermissionDetails(PermissionGroupCode.CATEGORIES, "Quản lý thông báo chung")]
        THONGBAO_QUANLY = 140,

        [PermissionDetails(PermissionGroupCode.CATEGORIES, "Quản lý danh sách trao đổi")]
        BINHLUAN_QUANLY = 145,

        [PermissionDetails(PermissionGroupCode.CATEGORIES, "Quản lý danh mục tiêu chuẩn")]
        TIEUCHUAN_QUANLY = 149,

        [PermissionDetails(PermissionGroupCode.PROJECT, "Xem tất cả dự án")]
        DuAn_VIEWALL = 155,

        [PermissionDetails(PermissionGroupCode.PROJECT, "Xem khách hàng trong dự án")]
        DUAN_XEM_KHACHHANG = 193,



        [PermissionDetails(PermissionGroupCode.SYSTEM, "Xem d/s nhóm nhân viên")]
        [Description("Xem nhóm nhân viên")]
        NHOMNHANVIEN_XEM = 105,

        [PermissionDetails(PermissionGroupCode.SYSTEM, "Cập nhật nhóm nhân viên")]
        [Description("Cập nhật nhóm nhân viên")]
        NHOMNHANVIEN_CAPNHAT = 106,

        [PermissionDetails(PermissionGroupCode.SYSTEM, "Xóa nhóm nhân viên")]
        [Description("Xóa nhóm nhân viên")]
        NHOMNHANVIEN_XOA = 107,


        [PermissionDetails(PermissionGroupCode.SYSTEM, "Đồng bộ dữ liệu")]
        [Description("Đồng bộ dữ liệu")]
        SYN_DATA = 108,

        //***
        // VNL PERMISSION CODE
        //***

        [PermissionDetails(PermissionGroupCode.CONTRACT, "Xem hợp đồng")]
        HOPDONG_XEM = 300,

        [PermissionDetails(PermissionGroupCode.CONTRACT, "Xóa hợp đồng")]
        HOPDONG_XOA = 301,

        [PermissionDetails(PermissionGroupCode.CONTRACT, "Thêm/cập nhập hợp đồng")]
        HOPDONG_LAPHOPDONG = 302,

        [PermissionDetails(PermissionGroupCode.CONTRACT, "Phê duyệt hợp đồng")]
        HOPDONG_PHEDUYET = 303,

       

        [PermissionDetails(PermissionGroupCode.CONTRACT_TEMPLATE, "Thêm/cập nhật mẫu hợp đồng")]
        MAUHOPDONG_SUA = 305,

        [PermissionDetails(PermissionGroupCode.CONTRACT_TEMPLATE, "Xóa mẫu hợp đồng")]
        MAUHOPDONG_XOA = 306,

        [PermissionDetails(PermissionGroupCode.CONTRACT_TEMPLATE, "Xem mẫu hợp đồng")]
        MAUHOPDONG_VIEW = 307,

     
        [PermissionDetails(PermissionGroupCode.LIBRARY, "Xem thư viện")]
        THUVIEN_XEM = 311,

        [PermissionDetails(PermissionGroupCode.LIBRARY, "Thêm/cập nhật thư viện")]
        THUVIEN_CAPNHAT = 312,

        [PermissionDetails(PermissionGroupCode.LIBRARY, "Xóa thư viện")]
        THUVIEN_XOA = 313,
       
        [PermissionDetails(PermissionGroupCode.QUOTE, "Xem báo giá")]
        BAOGIA_XEM = 315,

        [PermissionDetails(PermissionGroupCode.QUOTE, "Thêm/cập nhật báo giá")]
        BAOGIA_CAPNHAT = 316,

        [PermissionDetails(PermissionGroupCode.QUOTE, "Xóa báo giá")]
        BAOGIA_XOA = 317,

        [PermissionDetails(PermissionGroupCode.QUOTE, "Phê duyệt báo giá")]
        BAOGIA_PHEDUYET = 318,

        [PermissionDetails(PermissionGroupCode.QUOTE, "Cập nhật báo giá đã phê duyệt")]
        BAOGIA_CAPNHATDAPHEDUYET = 319,

        [PermissionDetails(PermissionGroupCode.CONTRACT, "Trình duyệt hợp đồng")]
        HOPDONG_TRINHDUYET = 320,

        [PermissionDetails(PermissionGroupCode.QUOTE, "Trình duyệt báo giá")]
        BAOGIA_TRINHDUYET = 321
    }
}