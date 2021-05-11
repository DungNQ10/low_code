using Lowcode.Framework.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Lowcode.Domain.Modules.CodeIt
{
   public class DynamicQueryResult
    {
        public DataTable List { get; set; }
        public PagingInfo Paging { get; set; }
    }
}
