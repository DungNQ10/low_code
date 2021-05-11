using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lowcode.Api.Modules
{
    public class ResultBase<T>
    {
        public bool success { set; get; }
        public T data { set; get; }
        public string message { set; get; }
        public int status_code { set; get; } = 200;
    }
}