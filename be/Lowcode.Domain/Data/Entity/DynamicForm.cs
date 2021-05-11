using System;
using System.Collections.Generic;
using System.Text;

namespace Lowcode.Domain.Data.Entity
{
    public class DynamicForm
    {



        public Int32 Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string TableName { get; set; }

        public string Content { get; set; }

        public string Path { get; set; }

        public string ApiInfo { get; set; }

    }
}
