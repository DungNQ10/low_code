using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Lowcode.Domain.Validators
{
    public class CharacterRangeAttribute : ValidationAttribute
    {
        public char[] AllowableValues { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is char))
                return new ValidationResult($"{validationContext.DisplayName}: Ký tự không hợp lệ");

            if (AllowableValues?.Contains(Convert.ToChar(value)) == true)
            {
                return ValidationResult.Success;
            }

            var msg = $"{validationContext.DisplayName}: Bạn chỉ được phép nhập các giá trị: { string.Join(", ", (AllowableValues)) }.";
            return new ValidationResult(msg);
        }
    }
}