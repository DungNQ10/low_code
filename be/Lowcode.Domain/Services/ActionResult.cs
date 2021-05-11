using System;

namespace Lowcode.Domain.Services
{
    public class ActionResult
    {
        public bool Success { set; get; }

        public string Message { set; get; }

        public object Data { set; get; }

        public static implicit operator decimal(ActionResult v)
        {
            throw new NotImplementedException();
        }
    }

    public class ActionResultType<T> : ActionResult
    {
        public new T Data { set; get; }
    }
}