using Lowcode.Domain.Data;
using Lowcode.Domain.Data.Entity;
using Lowcode.Framework.Data;
using Lowcode.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Lowcode.Domain.Services.DynamicFormFacade.Implementation
{
    public class DynamicFormService : IDynamicFormService
    {
        IEntityRepository<DynamicForm, DomainContext> _DynamicFormRepository;
         public DynamicFormService(IEntityRepository<DynamicForm, DomainContext> DynamicFormRepository)
        {
            _DynamicFormRepository = DynamicFormRepository;
        }

        public ActionResultType<bool> Delete(int id)
        {

            var item = _DynamicFormRepository.GetById(id);
            var res = _DynamicFormRepository.Delete(item);
            return new ActionResultType<bool>() { Success= res};
        }

        public ActionResultType< List<DynamicFormModel>> FillterModel(FilterDynamicFormModel filter)
        {
            using (var db = _DynamicFormRepository.GetDbContext())
            {
                var q = from o in db.DynamicForm
                        select o;
                if (!string.IsNullOrEmpty( filter.Search))
                {
                  //  q = q.Where(c => c.Name.Contains(filter.Search) || c.Code.Contains(filter.Search));
                }
                var list = q.OrderByDescending(c => c.Id).Skip(filter.Paging.StartRowIndex).Take(filter.Paging.PageSize).ToList();
                filter.Paging.RowsCount = q.Count();
                var listModels = list.CloneToListModels<DynamicForm, DynamicFormModel>();
               
                return new ActionResultType<List<DynamicFormModel>>() { Success=true, Data= listModels};
            }
        }

        public ActionResultType< DynamicFormModel> GetById(int id)
        {
            var item = _DynamicFormRepository.GetById(id);
            var itemModel = item.CloneToModel<DynamicForm, DynamicFormModel>();
            //using (var context =_DynamicFormRepository.GetDbContext())
            //{
            //    var apis = context.DynamicFormApi.Where(c => c.FormId == item.Id).ToList();
            //    if (apis.Count > 0)
            //    {
            //        itemModel.DynamicFormApis = apis.CloneToListModels<DynamicFormApi, DynamicFormApiModel>();
            //    }
            //}
            return new ActionResultType<DynamicFormModel>() { Success= true, Data= itemModel };
        }

        public ActionResult Save(DynamicFormModel model)
        {
            
            var item = model.CloneToModel<DynamicFormModel, DynamicForm>();
            if (model.Id==0)
            {
                var res = _DynamicFormRepository.Insert(item);
                return new ActionResult() { Success= res };
            }
            else
            {
                var res = _DynamicFormRepository.Update(item);
                return new ActionResult() { Success= res };
            }
        }

		public ActionResultType<List< DynamicFormModel>> GetAll()
        {
            var items = _DynamicFormRepository.Fetch();
            var itemModel = items.CloneToListModels<DynamicForm, DynamicFormModel>();
            return new ActionResultType<List< DynamicFormModel>>() { Success= true, Data= itemModel };
        }
    }
}
