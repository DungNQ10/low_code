using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Lowcode.Api.Helpers
{
    public class DecimalModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.BinderType == typeof(decimal?))
            {
                return new DecimalModelBinder();
            }

            return null;
        }
    }

    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext
                 .ValueProvider
                 .GetValue(bindingContext.ModelName);

            var cultureInfo = new CultureInfo("vi"); // Norwegian

            decimal.TryParse(
                valueProviderResult.FirstValue,
                // add more NumberStyles as necessary
                NumberStyles.AllowDecimalPoint,
                cultureInfo,
                out var model);

            bindingContext
                .ModelState
                .SetModelValue(bindingContext.ModelName, valueProviderResult);

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}
