using Lowcode.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lowcode.Domain.Modules.CodeIt
{
    public class ColumnInfo
    {

        public string ColumnName { get; set; }
        public string ColumnNameCamel
        {
            get
            {
                return ColumnName.ToCamelCase();
            }
        }

        public string DataType { get; set; }
        public string CodeDataType
        {
            get
            {
                var result = DataType;
                if (DataType == "money" || DataType == "float")
                {
                    result = "double";
                }
                else if (DataType == "decimal")
                {
                    result = "decimal";
                }
                else if (DataType == "bigint")
                {
                    result = "Int64";
                }
                else if (DataType == "int")
                {
                    result = "Int32";
                }
                else if (DataType == "smallint")
                {
                    result = "short";
                }
                else if (DataType == "tinyint")
                {
                    result = "byte";
                }
                else if ((DataType == "datetime" || DataType == "date"))
                {
                    result = "DateTime";
                }
                else if (DataType == "varchar" || DataType == "nvarchar" || DataType == "nchar" || DataType == "text" || DataType == "ntext")
                {
                    result = "string";
                }
                else if (DataType == "bit")
                {
                    result = "Boolean";
                }
                return result;

            }
        }
        public string DataTypeForCode
        {
            get
            {
                if (CodeDataType == "string")
                {
                    return CodeDataType;
                }
                else
                    return CodeDataType + ((IsNullable == true) ? "?" : string.Empty);
            }
        }
        public int? MaxLength { get; set; }
        public bool IsNullable { get; set; }
        public bool IsDateTime
        {
            get
            {


                return (DataType == "datetime" || DataType == "date");

            }
        }
        public bool IsString
        {
            get
            {

                return (DataType == "varchar" || DataType == "nvarchar" || DataType == "text" || DataType == "ntext");
            }
        }
        public bool IsBoolean
        {
            get
            {

                return (DataType == "bit");
            }
        }
        public bool IsNumber
        {

            get
            {

                return (DataType == "money" || DataType == "bigint" || DataType == "float" || DataType == "int");
            }
        }

        public bool isInputText
        {
            get
            {
                return (IsString && MaxLength != null && MaxLength <= 400 && MaxLength != -1);
            }

        }
        public bool isTextArea
        {
            get
            {
                return (IsString && MaxLength != null && MaxLength > 400 && MaxLength < 500);
            }

        }
        public bool isHtmlEditor
        {
            get
            {
                return (IsString && MaxLength != null && MaxLength == -1);
            }

        }
        public bool hasMaxLength
        {
            get
            {
                return MaxLength.HasValue ? true : false;
            }
        }
    }
}
