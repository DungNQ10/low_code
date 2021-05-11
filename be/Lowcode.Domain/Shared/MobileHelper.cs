using System;

namespace Lowcode.Domain.Shared
{
    /// <summary>
    /// Tiện ích chuyển đổi sđt thành tiêu chuẩn
    /// </summary>
    public class MobileHelper
    {
        /// <summary>
        /// Chuyển số điện thoại thành số tiêu chuẩn có định dạng 84916010184
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static string ToStandardMobile(string mobile)
        {
            //tu dong chuyen so dien thoai sang dinh dang tieu chuan +84
            if (!String.IsNullOrEmpty(mobile))
            {
                mobile = mobile.Replace("+", "");//+84 remove dau +
                if (mobile.StartsWith("0"))
                {
                    mobile = "84" + mobile.Substring(1);//0942342342 remove so 0 va thay bang 84
                }

                if (!mobile.StartsWith("0") && !mobile.StartsWith("84"))
                {
                    mobile = "84" + mobile;//94234234 them 84
                }
            }

            return mobile;
        }
    }
}