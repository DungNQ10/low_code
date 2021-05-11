namespace Lowcode.Domain.Services.AttachmentFacade
{
    public class DownloadFileModel
    {
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }

        public DownloadFileModel()
        {
            ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        }
    }
}