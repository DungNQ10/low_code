using Lowcode.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lowcode.Api.Modules
{
    public class PagedList<T>
    {
        public List<T> list { set; get; }

        public PagingInfo pager { set; get; }
    }
}