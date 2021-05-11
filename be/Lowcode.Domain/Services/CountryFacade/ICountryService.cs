using System;
using System.Collections.Generic;
using System.Text;
using Lowcode.Framework.Data;

namespace Lowcode.Domain.Services.CountryFacade
{
    public interface ICountryService
    {
        ActionResultType<bool> Delete(int id);
        ActionResultType<CountryModel> GetById(int id);
        ActionResult Save(CountryModel model);
        ActionResultType<List<CountryModel>> FillterModel(FilterCountryModel filter);
		ActionResultType<List<CountryModel>> GetAll();
    }
}
