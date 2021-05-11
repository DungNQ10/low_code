using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lowcode.Api.Helpers
{
    public class InvailidAccessException : Exception
    {
        public InvailidAccessException(string message)
        : base(message)
        {
        }

        public InvailidAccessException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
