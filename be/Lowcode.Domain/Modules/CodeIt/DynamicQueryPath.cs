using System;
using System.Collections.Generic;
using System.Text;

namespace Lowcode.Domain.Modules.CodeIt
{
  public  class DynamicQueryPath
    {
        public string Select { get; set; }
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public string WhereCondition { get; set; }
        public List<OrderPath> OrderPaths { get; set; }
        public List<ParamInfo> Paramaters { get; set; }
    }

   public class OrderPath
    {
        public string Name { get; set; }
        public OrderType Order { get; set; }
    }

    public enum OrderType
    {
        ASC=1,
        DESC=2
    }
}
