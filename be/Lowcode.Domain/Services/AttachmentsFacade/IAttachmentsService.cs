using System.Collections.Generic;

namespace Lowcode.Domain.Services.AttachmentsFacade
{
    public interface IAttachmentsService
    {
        ActionResultType<bool> Delete(int id);

        ActionResultType<AttachmentsModel> GetById(int id);

        ActionResult Save(AttachmentsModel model);

        ActionResultType<List<AttachmentsModel>> FillterModel(FilterAttachmentsModel filter);

        ActionResultType<List<AttachmentsModel>> GetAttachments(string tableName, int itemId);

        ActionResult SaveList(List<AttachmentsModel> models);
    }
}