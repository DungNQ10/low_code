using System;
using Lowcode.Framework.Data;

namespace Lowcode.Domain.Services
{
    public class BaseFilterModel
    {
        public PagingInfo Paging { get; set; }
        public string Search { get; set; }
        public Object SummaryRow { get; set; }
    }
}