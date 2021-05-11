using System;
using System.Collections.Generic;
using System.Text;
using Lowcode.Framework.Data;

namespace Lowcode.Domain.Services.DynamicFormFacade
{
    public interface IDynamicFormService
    {
        ActionResultType<bool> Delete(int id);
        ActionResultType<DynamicFormModel> GetById(int id);
        ActionResult Save(DynamicFormModel model);
        ActionResultType<List<DynamicFormModel>> FillterModel(FilterDynamicFormModel filter);
		ActionResultType<List<DynamicFormModel>> GetAll();
    }
}
