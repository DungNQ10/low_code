using System;

namespace Lowcode.Domain.Services.AttachmentsFacade
{
    public class AttachmentsModel
    {
        public int Id { get; set; }

        public string TableName { get; set; }

        public int? ItemId { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string ContentType { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}