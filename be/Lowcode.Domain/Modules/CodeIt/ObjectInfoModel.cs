using System;
using System.Collections.Generic;
using System.Text;

namespace Lowcode.Domain.Modules.CodeIt
{
    public class ObjectInfoModel
    {
        public string TableName { get; set; }
        public List<FieldInfoModel> Fields { get; set; }
    }
}
