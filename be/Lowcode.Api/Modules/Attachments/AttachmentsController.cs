using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Lowcode.Api.Helpers;
using Lowcode.Framework.Data;
//using Lowcode.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Lowcode.Domain.Services.AttachmentsFacade;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Lowcode.Domain.Shared;

namespace Lowcode.Api.Modules.Attachmentsss
{

    [Produces("application/json")]
    [Route("v1/Attachments")]
    public class AttachmentsController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly IAttachmentsService _AttachmentsService;
        public AttachmentsController(IHostingEnvironment hostingEnvironment, IAttachmentsService AttachmentsService)
        {
            _hostingEnvironment = hostingEnvironment;
            _AttachmentsService = AttachmentsService;
        }
        /// <summary>
        /// Upload file vào thư mục temp và trả về file key
        /// </summary>
        /// <returns></returns>
        /// <remarks>API upload file</remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>

        [Route("upload")]
        [HttpPost]

        public ActionResult UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                var modelStr = Request.Form["model"];



                string folderName = "Upload";
                string webRootPath = _hostingEnvironment.ContentRootPath;
                string newPath = string.IsNullOrWhiteSpace(Constant.UploadFolder) ? Path.Combine(webRootPath, folderName) : Constant.UploadFolder;
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    if (string.IsNullOrEmpty(modelStr))
                    {

                        AttachmentsModel model = new AttachmentsModel()
                        {
                            FileName = file.FileName,
                            ContentType = file.ContentType,
                            CreatedDate = DateTime.Now,
                            FilePath = Path.Combine(newPath, Guid.NewGuid() + "." + Path.GetExtension(file.FileName))
                        };
                        string fileName = file.FileName;
                        string fullPath = model.FilePath;
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        return Json(new ResultBase<AttachmentsModel>() { success = true, data = model });
                    }
                    else
                    {
                        var model = JsonConvert.DeserializeObject<AttachmentConfigModel>(modelStr);
                        if (!Directory.Exists(model.SavePath))
                        {
                            return Json(new ResultBase<AttachmentsModel>()
                            { success = false, message = "Thư mục không tồn tại." });
                        }
                        else
                        {
                            var path = Path.Combine(model.SavePath, file.FileName);
                            if (System.IO.File.Exists(path))
                            {
                                return Json(new ResultBase<AttachmentsModel>() { success = false, message = "File đã tồn tại." });
                            }
                            else
                            {
                                using (var stream = new FileStream(path, FileMode.Create))
                                {
                                    file.CopyTo(stream);
                                }
                                AttachmentsModel attachmentModel = new AttachmentsModel()
                                {
                                    FileName = file.FileName,
                                    ContentType = file.ContentType,
                                    CreatedDate = DateTime.Now,
                                    FilePath = path
                                };

                                return Json(new ResultBase<AttachmentsModel>() { success = true, data = attachmentModel });
                            }
                        }
                    }

                }

                return Json(new ResultBase<AttachmentsModel>() { success = true, data = null });

            }
            catch (System.Exception ex)
            {
                return Json("Upload Failed: " + ex.Message);
            }
        }

        [Route("getAttachmentss")]
        [HttpGet]
        public ActionResult GetAttachmentss(string tableName, int itemId)
        {
            var res = _AttachmentsService.GetAttachments(tableName, itemId);
            return Json(new ResultBase<List<AttachmentsModel>>() { data = res.Data, success = res.Success });
        }

        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> Download(int id)
        {
            var res = _AttachmentsService.GetById(id);
            if (res.Success == true)
            {
                var file = res.Data;
                var memory = new MemoryStream();
                using (var stream = new FileStream(file.FilePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, file.ContentType, file.FileName);
            }
            else
            {
                return Content("filename not present");
            }
        }

        [HttpGet]
        [Route("downloadExport")]
        public async Task<IActionResult> DownloadExport(string filePath, string fileName, string contentType)
        {
            var res = System.IO.File.Exists(filePath);
            if (res == true)
            {
                var memory = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                System.IO.File.Delete(filePath);
                return File(memory, contentType, fileName);
            }
            else
            {
                return Content("filename not present");
            }
        }

        [HttpGet]
        [Route("downloadPrint")]
        public async Task<IActionResult> DownloadPrint(string fileId)
        {
            string folderName = "Exports";
            string webRootPath = _hostingEnvironment.ContentRootPath;
            string newPath = Path.Combine(webRootPath, folderName);

            var filePath = Path.Combine(newPath, fileId);
            var res = System.IO.File.Exists(filePath);
            if (res == true)
            {
                var memory = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                System.IO.File.Delete(filePath);
                return File(memory, "application/pdf", fileId);
            }
            else
            {
                return Content("filename not present");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody]int id)
        {
            var res = _AttachmentsService.Delete(id);
            return Json(new ResultBase<bool>() { success = res.Success, data = res.Data, message = res.Message });
        }
    }

    public class AttachmentConfigModel
    {
        public string SavePath { get; set; }
    }
}