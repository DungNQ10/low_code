namespace CT.PMKT.Domain.Services
{
    /// <summary>
    /// Base cho toàn bộ model của ứng dụng
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// Kiểm tra tính hợp lệ của model
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual bool Valid(ref string message)
        {
            return true;
        }
    }
}