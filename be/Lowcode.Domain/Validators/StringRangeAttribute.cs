using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Lowcode.Domain.Validators
{
    public class StringRangeAttribute : ValidationAttribute
    {
        public string[] AllowableValues { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (AllowableValues?.Contains(value?.ToString()) == true || value == null)
            {
                return ValidationResult.Success;
            }

            var msg = $"{validationContext.DisplayName}: Bạn chỉ được phép nhập các giá trị: {string.Join(", ", (AllowableValues ?? new string[] { "Không tìm thấy giá trị hợp lệ" }))}.";
            return new ValidationResult(msg);
        }
    }
}