using Lowcode.Framework.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lowcode.Domain.Modules.CodeIt
{
   public class DynamicQueryModel
    {
        public List<ParamInfo> Paramaters { get; set; }
        public string Query { get; set; }
        public PagingInfo Paging { get; set; }
    }
}
