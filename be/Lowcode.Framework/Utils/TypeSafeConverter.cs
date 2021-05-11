using System;

namespace Lowcode.Framework.Utils
{
    /// <summary>
    /// The class is a helper class to do type-safe conversion
    /// We should avoid to use any direct type conversion, instead to collect it
    /// in one place for easier to do code maintenance in the future.
    /// </summary>
    public static class TypeSafeConverter
    {
        /// <summary>
        /// Function to convert an string type to Boolean.
        /// </summary>
        /// <param name="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="bool"/> value, if fail return default value of Boolean class: false</returns>
        public static bool ToBoolean(string inputValue)
        {
            bool blnReturnValue = default(bool);
            if (!string.IsNullOrEmpty(inputValue))
            {
                Boolean.TryParse(inputValue, out blnReturnValue);
            }
            return blnReturnValue;
        }

        /// <summary>
        /// Function to convert an string type to Byte.
        /// </summary>
        /// <param name="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="byte"/> value, if fail return default value of Byte class: 0</returns>
        public static byte ToByte(string inputValue)
        {
            byte bytReturnValue = default(byte);
            if (!string.IsNullOrEmpty(inputValue))
            {
                byte.TryParse(inputValue, out bytReturnValue);
            }
            return bytReturnValue;
        }

        /// <summary>
        /// Function to convert an string type to Signed Byte.
        /// </summary>
        /// <param name="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="sbyte"/> value, if fail return default value of SByte class: 0</returns>
        public static sbyte ToSByte(string inputValue)
        {
            sbyte sbytReturnValue = default(sbyte);
            if (!string.IsNullOrEmpty(inputValue))
            {
                sbyte.TryParse(inputValue, out sbytReturnValue);
            }
            return sbytReturnValue;
        }

        /// <summary>
        /// Function to convert an string type to Integer.
        /// </summary>
        /// <param name="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="short"/> value, if fail return default value of Int16 class: 0</returns>
        public static short ToInt16(string inputValue)
        {
            short shrReturnValue = default(short);
            if (!string.IsNullOrEmpty(inputValue))
            {
                short.TryParse(inputValue, out shrReturnValue);
            }
            return shrReturnValue;
        }

        /// <summary>
        /// Function to convert an string type to Unsigned Integer.
        /// </summary>
        /// <param name="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="ushort"/> value, if fail return default value of UInt16 class: 0</returns>
        public static ushort ToUInt16(string inputValue)
        {
            ushort ushrReturnValue = default(ushort);
            if (!string.IsNullOrEmpty(inputValue))
            {
                ushort.TryParse(inputValue, out ushrReturnValue);
            }
            return ushrReturnValue;
        }

        /// <summary>
        /// Function to convert an string type to Integer.
        /// </summary>
        /// <param name="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="int"/> value, if fail return default value of Int32 class: 0</returns>
        public static int ToInt32(string inputValue)
        {
            int intReturnValue = default(int);
            if (!string.IsNullOrEmpty(inputValue))
            {
                int.TryParse(inputValue, out intReturnValue);
            }
            return intReturnValue;
        }

        /// <summary>
        /// Function to convert an string type to Unsigned Integer.
        /// </summary>
        /// <param name="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="uint"/> value, if fail return default value of UInt32 class: 0</returns>
        public static uint ToUInt32(string inputValue)
        {
            uint uintReturnValue = default(uint);
            if (!string.IsNullOrEmpty(inputValue))
            {
                uint.TryParse(inputValue, out uintReturnValue);
            }
            return uintReturnValue;
        }

        /// <summary>
        /// Function to convert an string type to Long Integer.
        /// </summary>
        /// <param name="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="long"/> value, if fail return default value of Int64 class: 0</returns>
        public static long ToInt64(string inputValue)
        {
            long lngReturnValue = default(long);
            if (!string.IsNullOrEmpty(inputValue))
            {
                long.TryParse(inputValue, out lngReturnValue);
            }
            return lngReturnValue;
        }


        /// <summary>
        /// Function to convert an string type to Unsigned Long Integer.
        /// </summary>
        /// <param name="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="ulong"/> value, if fail return default value of UInt64 class: 0</returns>
        public static ulong ToUInt64(string inputValue)
        {
            ulong ulngReturnValue = default(ulong);
            if (!string.IsNullOrEmpty(inputValue))
            {
                ulong.TryParse(inputValue, out ulngReturnValue);
            }
            return ulngReturnValue;
        }


        /// <summary>
        /// Function to convert an string type to Single-Precision Floating-Point.
        /// </summary>
        /// <param name="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="float"/> value, if fail return default value of Single class: 0.0f</returns>
        public static float ToFloat(string inputValue)
        {
            float fltReturnValue = default(float);
            if (!string.IsNullOrEmpty(inputValue))
            {
                float.TryParse(inputValue, out fltReturnValue);
            }
            return fltReturnValue;
        }

        /// <summary>
        /// Function to convert an string type to Double-Precision Floating-Point.
        /// </summary>
        /// <param name="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="double"/> value, if fail return default value of Double class: 0.0d</returns>
        public static double ToDouble(string inputValue)
        {
            double dblReturnValue = default(double);
            if (!string.IsNullOrEmpty(inputValue))
            {
                double.TryParse(inputValue, out dblReturnValue);
            }
            return dblReturnValue;
        }


        /// <summary>
        /// Function to convert an string type to Decimal.
        /// </summary>
        /// <param name="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="decimal"/> value, if fail return default value of Decimal class: 0.0m<returns>
        public static decimal ToDecimal(string inputValue)
        {
            decimal decReturnValue = default(decimal);
            if (!string.IsNullOrEmpty(inputValue))
            {
                decimal.TryParse(inputValue, out decReturnValue);
            }
            return decReturnValue;
        }

        /// <summary>
        /// Function to convert an string type to DateTime.
        /// </summary>
        /// <param name="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="DateTime"/> value, if fail return MinValue of DateTime class</returns>
        public static DateTime ToDateTime(string inputValue)
        {
            DateTime dtmReturnValue = new DateTime(1900, 1, 1);
            if (!string.IsNullOrEmpty(inputValue))
            {
                DateTime.TryParse(inputValue, out dtmReturnValue);
            }
            return dtmReturnValue;
        }

        #region Convert format datetime
        /// <summary>
        /// Ddmmyyyy_to_mmddyyyys the specified STR value.
        /// </summary>
        /// <param name="strValue">The STR value.</param>
        /// <returns></returns>
        public static string ddmmyyyy_to_mmddyyyy(string strValue)
        {
            if (strValue != "")
            {
                string[] strArray = strValue.Split(new char[] { '/' });
                string str = strArray[0];
                string str2 = strArray[1];
                string str3 = strArray[2];
                return (str2 + "/" + str + "/" + str3);
            }
            return "";
        }

        public static string mmddyyyy_to_ddmmyyyy(string strValue)
        {
            if (strValue != "")
            {
                string[] strArray = strValue.Split(new char[] { '/' });
                string str = strArray[0];
                string str2 = strArray[1];
                string str3 = strArray[2];
                return (str2 + "/" + str + "/" + str3);
            }
            return "";
        }
        #endregion
    }
}
