using Lowcode.Domain.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lowcode.Domain.Data.Entity
{
   public class DynamicFormApi
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public ApiType ApiType { get; set; }
        public string ApiUrl { get; set; }
        public string ApiQuery { get; set; }
        public string  ApiParameters { get; set; }
    }
}
