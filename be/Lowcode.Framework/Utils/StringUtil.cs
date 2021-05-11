using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace Lowcode.Framework.Utils
{
    /// <summary>
    /// Declare utility functions to process string type.
    /// </summary>
    public static class StringUtil
    {
        #region "constants"
        /// <summary>
        /// Return a Regular Expressions to validate a user name string.
        /// </summary>
        public static readonly Regex USERNAME_REGX = new Regex(@"^[\w\-\.]+$", RegexOptions.Compiled);

        /// <summary>
        /// Return a Regular Expressions to validate a e-mail string.
        /// </summary>
        public static readonly Regex EMAIL_REGX = new Regex(@"^\w+([\-\+\.]\w+)*@\w+([\-\.]\w+)*\.\w+([\-\.]\w+)*$",
            RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// Return a Regular Expressions to validate a Hexa string.
        /// </summary>
        public static readonly Regex HEXA_REGX = new Regex(@"^[0-9a-fA-F]+$", RegexOptions.Compiled);

        /// <summary>
        /// Return a Regular Expressions to validate a mobile number string.
        /// </summary>
        public static readonly Regex MOBILE_REGX = new Regex(@"^(0|\+[1-9])[0-9]+$", RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// Returns a Regular Expressions to validate an absolute URL string.
        /// </summary>
        public static readonly Regex URL_REGX = new Regex(@"^((ht|f)tp(s?)\:\/\/)?([\w]+:\w+@)?([a-zA-Z]{1}([\w\-]+\.?)+([\w]{2,5})?|(2[0-4]\d|25[0-5]|[01]?\d\d?)\.(2[0-4]\d|25[0-5]|[01]?\d\d?)\.(2[0-4]\d|25[0-5]|[01]?\d\d?)\.(2[0-4]\d|25[0-5]|[01]?\d\d?))(:[\d]{1,5})?((/[\w,%\-]+)*/?)([\w%\-]+\.[\w]{3,4})?(\?\w+=[\w%]+(&\w+=[\w%]+)*|\?\w+)?(\#[\w,%\-]*)?$",
            RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

        public static string Encode(string encode, EncodeSeperator seperator, bool replaceSpecialCharacter, bool? toLower = true)
        {
            try
            {
                //Remove VietNamese character
                if (toLower == true)
                    encode = encode.Trim().ToLower();

                encode = Regex.Replace(encode, "[áàảãạâấầẩẫậăắằẳẵặạ]", "a");
                encode = Regex.Replace(encode, "[éèẻẽẹêếềểễệ]", "e");
                encode = Regex.Replace(encode, "[iíìỉĩị]", "i");
                encode = Regex.Replace(encode, "[óòỏõọơớờởỡợôốồổỗộ]", "o");
                encode = Regex.Replace(encode, "[úùủũụưứừửữự]", "u");
                encode = Regex.Replace(encode, "[yýỳỷỹỵ]", "y");
                encode = Regex.Replace(encode, "[đ]", "d");

                if (replaceSpecialCharacter)
                {
                    encode = Regex.Replace(encode, "[–]", "");
                    encode = Regex.Replace(encode, "[]]", "");
                    encode = Regex.Replace(encode, "[[]", "");
                    encode = Regex.Replace(encode, "[/]", " ");
                }

                encode = encode.Replace("ạ", "a");
                if (encode.IndexOf("ạ") > 0)
                {
                    throw new ArgumentException();
                }
                //Remove space
                encode = encode.Replace(" ", "-");

                //Remove special character
                if (replaceSpecialCharacter)
                    encode = Regex.Replace(encode, "[:\"`~!@#$%^&*()-+=?/>.<,{}[]|]\\']", "");

                if (encode.EndsWith("-"))
                {
                    encode = encode.Substring(0, encode.LastIndexOf('-'));
                }

                //Remove dupplicate - character
                string exp = "[-]{2,}";
                encode = Regex.Replace(encode, exp, "-");

                if (seperator == EncodeSeperator.Space)
                {
                    encode = encode.Replace('-', ' ');
                }

                return encode;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Returns a Regular Expressions to validate a relative URL string.
        /// </summary>
        public static readonly Regex RELURL_REGX = new Regex(@"^((\~|\.*)\/)?([\w,%\-]+\/)*([\w%\-]+\.\w+)?(\?\w+=[\w%]+(&\w+=[\w%]+)*|\?\w+)?(\#[\w,%\-]*)?$",
            RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

        /// <summary>
        /// Returns a Regular Expressions to validate a half-size string.
        /// </summary>
        public static readonly Regex HALFSIZE_REGX = new Regex("^[ -\xFE]+$", RegexOptions.Compiled);

        /// <summary>
        /// Returns an array of countries code (two-letter code defined in ISO 3166 for the country/region).
        /// </summary>
        public static readonly string[] COUNTRIES_CODE = "AD,AE,AF,AG,AI,AL,AM,AN,AO,AQ,AR,AS,AT,AU,AW,AX,AZ,BA,BB,BD,BE,BF,BG,BH,BI,BJ,BM,BN,BO,BR,BS,BT,BV,BW,BY,BZ,CA,CC,CD,CF,CG,CH,CI,CK,CL,CM,CN,CO,CR,CS,CU,CV,CX,CY,CZ,DE,DJ,DK,DM,DO,DZ,EC,EE,EG,EH,ER,ES,ET,FI,FJ,FK,FM,FO,FR,FX,GA,GB,GD,GE,GF,GH,GI,GL,GM,GN,GP,GQ,GR,GS,GT,GU,GW,GY,HK,HM,HN,HR,HT,HU,ID,IE,IL,IN,IO,IQ,IR,IS,IT,JM,JO,JP,KE,KG,KH,KI,KM,KN,KP,KR,KW,KY,KZ,LA,LB,LC,LI,LK,LR,LS,LT,LU,LV,LY,MA,MC,MD,MG,MH,MK,ML,MM,MN,MO,MP,MQ,MR,MS,MT,MU,MV,MW,MX,MY,MZ,NA,NC,NE,NF,NG,NI,NL,NO,NP,NR,NU,NZ,OM,PA,PE,PF,PG,PH,PK,PL,PM,PN,PR,PS,PT,PW,PY,QA,RE,RO,RU,RW,SA,SB,SC,SD,SE,SG,SH,SI,SJ,SK,SL,SM,SN,SO,SR,ST,SU,SV,SY,SZ,TC,TD,TF,TG,TH,TJ,TK,TL,TM,TN,TO,TP,TR,TT,TV,TW,TZ,UA,UG,UK,UM,US,UY,UZ,VA,VC,VE,VG,VI,VN,VU,WF,WS,YE,YT,YU,ZA,ZM,ZR,ZW".Split(',');

        /// <summary>
        /// Returns a Regular Expressions to validate a IP string.
        /// </summary>
        public static readonly Regex IP_REGX = new Regex(@"^(?<First>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Second>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Third>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Fourth>2[0-4]\d|25[0-5]|[01]?\d\d?)$",
            RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// Return a Regular Expressions to validate a 'Order by' clause in a HSQL string.
        /// </summary>
        public static readonly Regex ORDERBY_REGX = new Regex(@"^[\w\-\{\}]+\.[\w\-\{\}]+(\s+(asc|desc))?(,[\w\-\{\}]+\.[\w\-\{\}]+(\s+(asc|desc))?)*$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
        #endregion

        #region "validate patterns"
        /// <summary>
        /// Return a Regular Expressions to validate a positive number.
        /// </summary>
        public static readonly Regex P_NUMBER_REGX = new Regex("^[1-9][0-9]*$", RegexOptions.Compiled);
        /// <summary>
        /// Return a Regular Expressions to validate a non-negative number.
        /// </summary>
        public static readonly Regex NN_NUMBER_REGX = new Regex("^[0-9]+$", RegexOptions.Compiled);
        /// <summary>
        /// Return a Regular Expressions to validate a integer number.
        /// </summary>
        public static readonly Regex INTEGER_REGX = new Regex(@"^(\-|\+)?[0-9]+$", RegexOptions.Compiled | RegexOptions.ExplicitCapture);
        #endregion

        /// <summary>
        /// Escape a value in a HSQL (ex: LIKE '%\%' ESCAPE '\').
        /// </summary>
        /// <param name="str">String value to escape.</param>
        /// <returns>An escaped string.</returns>
        public static string EscapeInHSQL(string str)
        {
            if (str == null) throw new ArgumentNullException("str");
            str = str
                .Replace("\\_", "_")
                .Replace("_", "\\_")
                .Replace("\\%", "%")
                .Replace("%", "\\%")
                .Replace("?", "_")
                .Replace("*", "%");
            return str;
        }

        /// <summary>
        /// Loại bỏ thẻ Html khỏi văn bản html
        /// </summary>
        /// <param name="htmlDocument"></param>
        /// <returns></returns>
        public static string StripHtml(string htmlDocument)
        {
            String result = Regex.Replace(htmlDocument, @"<[^>]*>", String.Empty);
            return result;
        }

        /// <summary>
        /// Shorten a sentence by maximum length.
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="len"></param>
        /// <returns>A short sentence (padded by '...').</returns>
        public static string ShortenByWord(string sentence, int len)
        {
            if (sentence == null) return string.Empty;
            if (sentence.Length > len)
            {
                sentence = sentence.Substring(0, len);
                // cut a word
                int pos = sentence.LastIndexOf(' ');
                if (pos > 0) sentence = sentence.Substring(0, pos);
                return sentence + "...";
            }
            return sentence;
        }

        public static string ReplaceBadString(string strSource)
        {
            string str = strSource;
            if (!string.IsNullOrEmpty(str))
            {
                return str.Replace("'", "&#39;").Replace("\"", "&#34;").Replace("<", "&lt;").Replace(">", "&gt;");
            }
            else return str;
        }

        public static string ReplaceLineFeed(string strContent)
        {
            if (!string.IsNullOrEmpty(strContent))
            {
                return strContent.Replace("" + (char)13, "<br />");
            }
            else
            {
                return strContent;
            }
        }

        /// <summary>
        /// GenCode by date time
        /// </summary>
        /// <returns></returns>
        public static string GenCode()
        {
            string code = string.Empty;

            code += DateTime.Now.Year.ToString().Substring(2);

            code += DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month : DateTime.Now.Month + "";

            code += DateTime.Now.Day < 10 ? "0" + DateTime.Now.Day : DateTime.Now.Day + "";

            code += DateTime.Now.Hour < 10 ? "0" + DateTime.Now.Hour : DateTime.Now.Hour + "";

            code += DateTime.Now.Minute < 10 ? "0" + DateTime.Now.Minute : DateTime.Now.Minute + "";

            code += DateTime.Now.Second < 10 ? "0" + DateTime.Now.Second : DateTime.Now.Second + "";

            code += DateTime.Now.Millisecond + "";

            return code;
        }

        /// <summary>
        /// Gets the type file.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static string GetTypeFile(string type)
        {
            string strReturn = string.Empty;
            switch (type.ToUpper())
            {
                case "DOC":
                    strReturn = "Icon Icon3";
                    break;
                case "DOCX":
                    strReturn = "Icon Icon3";
                    break;
                case "PDF":
                    strReturn = "Icon Icon2";
                    break;
                case "XLS":
                    strReturn = "Icon";
                    break;
                default:
                    strReturn = "Icon Icon1";
                    break;
            }
            return strReturn;
        }

        /// <summary>
        /// Gets the user ID from user type value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int GetUserIDFromUserTypeValue(string value)
        {
            return Convert.ToInt32(value.Replace("#", "").Split(';')[0]);
        }

        /// <summary>
        /// Loại bỏ các ký tự đặc biệt và chuyển sang các ký tự và chữ số
        /// </summary>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string Encode(string encode)
        {
            try
            {
                //Remove VietNamese character
                encode = encode.Trim().ToLower();
                encode = Regex.Replace(encode, "[áàảãạâấầẩẫậăắằẳẵặ]", "a");
                encode = Regex.Replace(encode, "[éèẻẽẹêếềểễệ]", "e");
                encode = Regex.Replace(encode, "[iíìỉĩị]", "i");
                encode = Regex.Replace(encode, "[óòỏõọơớờởỡợôốồổỗộ]", "o");
                encode = Regex.Replace(encode, "[úùủũụưứừửữự]", "u");
                encode = Regex.Replace(encode, "[yýỳỷỹỵ]", "y");
                encode = Regex.Replace(encode, "[đ]", "d");

                //Remove space
                encode = encode.Replace(" ", "-");

                //Remove special character
                encode = Regex.Replace(encode, "[:\"`~!@#$%^&*()-+=?/>.<,{}[]|]\\'“”]", "");
                encode = encode.Replace("'", "");
                encode = encode.Replace("“", "");
                encode = encode.Replace("”", "");
                encode = encode.Replace("]", "");
                encode = encode.Replace("[", "");

                if (encode.EndsWith("-"))
                {
                    encode = encode.Substring(0, encode.LastIndexOf('-'));
                }

                //Remove dupplicate - character
                string exp = "[-]{2,}";
                encode = Regex.Replace(encode, exp, "-");

                string tmp = Regex.Replace(encode, "[^a-zA-Z0-9]+", "-");

                tmp = tmp.Replace("---", "--");
                tmp = tmp.Replace("--", "-");

                if (tmp.StartsWith("-"))
                    tmp = tmp.Substring(1);

                if (tmp.EndsWith("-"))
                    tmp = tmp.Substring(0, tmp.Length - 1);

                return tmp;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string Encode(string encode, EncodeSeperator seperator)
        {
            try
            {
                //Remove VietNamese character
                encode = encode.Trim().ToLower();
                encode = Regex.Replace(encode, "[áàảãạâấầẩẫậăắằẳẵặạ]", "a");
                encode = Regex.Replace(encode, "[éèẻẽẹêếềểễệ]", "e");
                encode = Regex.Replace(encode, "[iíìỉĩị]", "i");
                encode = Regex.Replace(encode, "[óòỏõọơớờởỡợôốồổỗộ]", "o");
                encode = Regex.Replace(encode, "[úùủũụưứừửữự]", "u");
                encode = Regex.Replace(encode, "[yýỳỷỹỵ]", "y");
                encode = Regex.Replace(encode, "[đ]", "d");
                encode = encode.Replace("ạ", "a");
                if (encode.IndexOf("ạ") > 0)
                {
                    throw new ArgumentException();
                }
                //Remove space
                encode = encode.Replace(" ", "-");

                //Remove special character
                encode = Regex.Replace(encode, "[:\"`~!@#$%^&*()-+=?/>.<,{}[]|]\\']", "");

                if (encode.EndsWith("-"))
                {
                    encode = encode.Substring(0, encode.LastIndexOf('-'));
                }

                //Remove dupplicate - character
                string exp = "[-]{2,}";
                encode = Regex.Replace(encode, exp, "-");

                if (seperator == EncodeSeperator.Space)
                {
                    encode = encode.Replace('-', ' ');
                }

                return encode;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string UpperCaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        /// <summary>
        /// Compares two objects for equivalence, ignoring the case of strings.
        /// </summary>
        [Serializable]
        public class CaseInsensitiveComparer<T> : IComparer<T>
        {
            private CompareInfo _compareInfo;
            private static CaseInsensitiveComparer<T> _invariantCaseInsensitiveComparer;

            /// <summary>
            /// Initializes a new instance of the CaseInsensitiveComparer class using the CurrentCulture of the current thread.
            /// </summary>
            public CaseInsensitiveComparer() : this(CultureInfo.CurrentCulture) { }

            /// <summary>
            /// Initializes a new instance of the CaseInsensitiveComparer class using the specified <see cref="CultureInfo" />.
            /// </summary>
            /// <param name="culture">The <see cref="CultureInfo" /> to use for the new CaseInsensitiveComparer.</param>
            /// <exception cref="ArgumentNullException">culture is null.</exception>
            public CaseInsensitiveComparer(CultureInfo culture)
            {
                if (culture == null) throw new ArgumentNullException("culture");
                this._compareInfo = culture.CompareInfo;
            }

            /// <summary>
            /// Performs a case-insensitive comparison of two objects of the same type and returns a value indicating whether
            /// one is less than, equal to or greater than the other.
            /// </summary>
            /// <returns>Value Condition Less than zero x is less than y, with casing ignored.
            /// Zero x equals y, with casing ignored. Greater than zero x is greater than y, with casing ignored.</returns>
            /// <param name="x">The first object to compare.</param>
            /// <param name="y">The second object to compare.</param>
            /// <exception cref="ArgumentException">Neither x nor y implements the <see cref="IComparable" /> interface.
            /// -or- x and y are of different types.</exception>
            public int Compare(T x, T y)
            {
                string str = x as string, str2 = y as string;
                if ((str != null) && (str2 != null)) return this._compareInfo.Compare(str, str2, CompareOptions.IgnoreCase);
                return Comparer<T>.Default.Compare(x, y);
            }

            /// <summary>
            /// Gets an instance of CaseInsensitiveComparer that is associated with the CurrentCulture of the current thread
            /// and that is always available.</summary>
            /// <returns>An instance of CaseInsensitiveComparer that is associated with the CurrentCulture of the current thread.</returns>
            public static CaseInsensitiveComparer<T> Default
            {
                get { return new CaseInsensitiveComparer<T>(); }
            }

            /// <summary>
            /// Gets an instance of CaseInsensitiveComparer that is associated with InvariantCulture and that is always available.
            /// </summary>
            /// <returns>An instance of CaseInsensitiveComparer that is associated with InvariantCulture.</returns>
            public static CaseInsensitiveComparer<T> DefaultInvariant
            {
                get
                {
                    if (_invariantCaseInsensitiveComparer == null)
                        _invariantCaseInsensitiveComparer = new CaseInsensitiveComparer<T>(CultureInfo.InvariantCulture);
                    return _invariantCaseInsensitiveComparer;
                }
            }
        }
    }

    public enum EncodeSeperator
    {
        /// <summary>
        /// Ngăn cách bằng khoảng trắng
        /// </summary>
        Space,

        /// <summary>
        /// Ngăn cách bằng dấu trừ
        /// </summary>
        Minus
    }

    public static class StringHelper
    {
        public static string[] RemoveDuplicates(this string[] s)
        {
            HashSet<string> set = new HashSet<string>(s);
            string[] result = new string[set.Count];
            set.CopyTo(result);
            return result;
        }

        public static int[] RemoveDuplicates(this int[] s)
        {
            HashSet<int> set = new HashSet<int>(s);
            int[] result = new int[set.Count];
            set.CopyTo(result);
            return result;
        }

        /// <summary>
        /// Chuyển chuỗi có định dạng ";3;4;3;6;6" thành list ids
        /// </summary>
        /// <param name="stringFormat"></param>
        /// <returns></returns>
        public static List<int> ParseIds(this string stringFormat)
        {
            if (String.IsNullOrEmpty(stringFormat) || !stringFormat.Contains(";")) return new List<int>();

            var ids = stringFormat.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Select(c => Convert.ToInt32(c)).ToList();

            return ids;
        }
    }
}
