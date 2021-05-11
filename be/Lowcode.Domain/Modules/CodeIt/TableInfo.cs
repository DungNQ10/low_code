using Lowcode.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lowcode.Domain.Modules.CodeIt
{
    public class TableInfo
    {
        public string TableName { get; set; }
        public string PrimaryKey { get; set; }
        public string PrimaryKeyCamel
        {
            get { return PrimaryKey.ToCamelCase(); }
        }

        public string TableNameLowerCase
        {
            get { return TableName.ToLower(); }
        }
        public string TableNameCamel
        {
            get { return TableName.ToCamelCase(); }
        }
        public List<ColumnInfo> ColumnInfos { get; set; }
        public string ProjectName { get; set; }
    }
}
