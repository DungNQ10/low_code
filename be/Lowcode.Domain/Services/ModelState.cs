using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.PMKT.Domain.Services
{
    /// <summary>
    /// Quản lý trạng thái của model
    /// </summary>
    public class ModelState
    {
        /// <summary>
        /// Kiểm tra tính hợp lệ của model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ActionResult IsValid(BaseModel model)
        {
            if (model == null)
                return new ActionResult() { Result = false, Error = "model không được null" };

            var message = string.Empty;
            var res = model.Valid(ref message);
            if (res)
                return new ActionResult() { Result = true };
            else
                return new ActionResult() { Result = false, Error = message };
        }
    }
}
