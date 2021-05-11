using System.Collections.Generic;
using System.Linq;

using Lowcode.Domain.Data;
using Lowcode.Domain.Data.Entity;
using Lowcode.Framework.Data;
using Lowcode.Framework.Utils;

namespace Lowcode.Domain.Services.AttachmentsFacade.Implementation
{
    public class AttachmentsService : IAttachmentsService
    {
        private IEntityRepository<Attachments, DomainContext> _AttachmentsRepository;

        public AttachmentsService(IEntityRepository<Attachments, DomainContext> AttachmentsRepository)
        {
            _AttachmentsRepository = AttachmentsRepository;
        }

        public ActionResultType<bool> Delete(int id)
        {
            var item = _AttachmentsRepository.GetById(id);
            var res = _AttachmentsRepository.Delete(item);
            return new ActionResultType<bool>() { Success = res };
        }

        public ActionResultType<List<AttachmentsModel>> FillterModel(FilterAttachmentsModel filter)
        {
            using (var db = _AttachmentsRepository.GetDbContext())
            {
                var q = from o in db.Attachments
                        select o;
                if (!string.IsNullOrEmpty(filter.Search))
                {
                    //  q = q.Where(c => c.Name.Contains(filter.Search) || c.Code.Contains(filter.Search));
                }
                var list = q.OrderByDescending(c => c.Id).Skip(filter.Paging.StartRowIndex).Take(filter.Paging.PageSize).ToList();
                filter.Paging.RowsCount = q.Count();
                var listModels = list.CloneToListModels<Attachments, AttachmentsModel>();

                return new ActionResultType<List<AttachmentsModel>>() { Success = true, Data = listModels };
            }
        }

        public ActionResultType<AttachmentsModel> GetById(int id)
        {
            var item = _AttachmentsRepository.GetById(id);
            var itemModel = item.CloneToModel<Attachments, AttachmentsModel>();
            return new ActionResultType<AttachmentsModel>() { Success = true, Data = itemModel };
        }

        public ActionResult Save(AttachmentsModel model)
        {
            var item = model.CloneToModel<AttachmentsModel, Attachments>();
            if (model.Id == 0)
            {
                var res = _AttachmentsRepository.Insert(item);
                return new ActionResult() { Success = res };
            }
            else
            {
                var res = _AttachmentsRepository.Update(item);
                return new ActionResult() { Success = res };
            }
        }

        public ActionResultType<List<AttachmentsModel>> GetAttachments(string tableName, int itemId)
        {
            using (var context = _AttachmentsRepository.GetDbContext())
            {
                var list = context.Attachments.Where(c => c.TableName == tableName && c.ItemId == itemId).ToList();
                var listModel = list.CloneToListModels<Attachments, AttachmentsModel>();
                return new ActionResultType<List<AttachmentsModel>>() { Data = listModel, Success = true };
            }
        }

        public ActionResult SaveList(List<AttachmentsModel> models)
        {
            using (var context = _AttachmentsRepository.GetDbContext())
            {
                if (models == null || models.Count == 0)
                {
                    return new ActionResult() { Success = true };
                }
                var entities = models.CloneToListModels<AttachmentsModel, Attachments>();
                context.Attachments.AddRange(entities);
                context.SaveChanges();
                return new ActionResult() { Success = true };
            }
        }
    }
}