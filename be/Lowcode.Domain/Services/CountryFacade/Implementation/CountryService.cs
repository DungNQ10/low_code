using Lowcode.Domain.Data;
using Lowcode.Domain.Data.Entity;
using Lowcode.Framework.Data;
using Lowcode.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Lowcode.Domain.Services.CountryFacade.Implementation
{
    public class CountryService : ICountryService
    {
        IEntityRepository<Country, DomainContext> _CountryRepository;
         public CountryService(IEntityRepository<Country, DomainContext> CountryRepository)
        {
            _CountryRepository = CountryRepository;
        }

        public ActionResultType<bool> Delete(int id)
        {

            var item = _CountryRepository.GetById(id);
           
            var res = _CountryRepository.Delete(item);
            return new ActionResultType<bool>() { Success= res};
        }

        public ActionResultType< List<CountryModel>> FillterModel(FilterCountryModel filter)
        {
            using (var db = _CountryRepository.GetDbContext())
            {
                var q = from o in db.Country
                        select o;
                if (!string.IsNullOrEmpty( filter.Search))
                {
                  //  q = q.Where(c => c.Name.Contains(filter.Search) || c.Code.Contains(filter.Search));
                }
                var list = q.OrderByDescending(c => c.Id).Skip(filter.Paging.StartRowIndex).Take(filter.Paging.PageSize).ToList();
                filter.Paging.RowsCount = q.Count();
                var listModels = list.CloneToListModels<Country, CountryModel>();
               
                return new ActionResultType<List<CountryModel>>() { Success=true, Data= listModels};
            }
        }

        public ActionResultType< CountryModel> GetById(int id)
        {
            var item = _CountryRepository.GetById(id);
            var itemModel = item.CloneToModel<Country, CountryModel>();
            return new ActionResultType<CountryModel>() { Success= true, Data= itemModel };
        }

        public ActionResult Save(CountryModel model)
        {
            
            var item = model.CloneToModel<CountryModel, Country>();
            if (model.Id==0)
            {
                var res = _CountryRepository.Insert(item);
                return new ActionResult() { Success= res };
            }
            else
            {
                var res = _CountryRepository.Update(item);
                return new ActionResult() { Success= res };
            }
        }

		public ActionResultType<List< CountryModel>> GetAll()
        {
            var items = _CountryRepository.Fetch();
            var itemModel = items.CloneToListModels<Country, CountryModel>();
            return new ActionResultType<List< CountryModel>>() { Success= true, Data= itemModel };
        }
    }
}
