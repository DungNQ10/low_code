using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Web.UI.WebControls;
using System.Web;
using System.IO;
using System.Configuration;
using System.Drawing.Imaging;

namespace NPC.PMS.Framework.Utils
{

    /// <summary>
    /// Invalid file exception
    /// </summary>
    public class InvalidFileException : Exception
    {
        public InvalidFileException()
            : base()
        {

        }

        public InvalidFileException(string message)
            : base(message)
        {

        }
    }

    /// <summary>
    /// Upload file component
    /// </summary>
    public static class UploadFileHelper
    {
        private static List<string> ValidExtensions// = new List<string> { ".doc", ".xls", ".jpg", ".gif", ".bmp", ".png", ".rar" };//Đuôi file hợp lệ
        {
            get
            {
                if (String.IsNullOrEmpty(ConfigurationManager.AppSettings["ValidExtensions"]))
                    throw new FrameworkException("Please config valid extensions for uploaded files");
                return new List<string>(ConfigurationManager.AppSettings["ValidExtensions"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        private static double Capacity// Dung lượng tính bằng MB
        {
            get
            {
                if (String.IsNullOrEmpty(ConfigurationManager.AppSettings["Capacity"]))
                    throw new FrameworkException("Please config capacity for uploaded files");
                return TypeSafeConverter.ToDouble(ConfigurationManager.AppSettings["Capacity"]);
            }
        }

        private static string FilePath
        {
            get
            {
                if (String.IsNullOrEmpty(ConfigurationManager.AppSettings["UploadFilePath"]))
                    throw new FrameworkException("Please config upload file path");
                return ConfigurationManager.AppSettings["UploadFilePath"];
            }
        }

        /// <summary>
        /// Uploads the specified file upload.
        /// </summary>
        /// <param name="fileUpload">The file upload.</param>
        public static string Upload(FileUpload fileUpload)
        {
            if (fileUpload.HasFile)
            {
                Upload(fileUpload.PostedFile);
            }

            return string.Empty;
        }

        /// <summary>
        /// Uploads the specified posted file.
        /// Component checks file's validation first.
        /// </summary>
        /// <param name="postedFile">The posted file.</param>
        public static string Upload(HttpPostedFile postedFile)
        {
            string fileName = Path.GetFileName(postedFile.FileName);
            //Xử lý lỗi tên file dài quá 240 ký tự
            if (fileName.Length >= 100)
            {
                fileName = fileName.Substring(fileName.Length - 100);
            }
            CheckValid(postedFile);

            //Kiem tra su ton tai cua file, neu ton tai se doi ten file
            var filePath = HttpContext.Current.Server.MapPath(FilePath) + Path.DirectorySeparatorChar + fileName;
            if (File.Exists(filePath))
            {
                fileName = DateTime.Now.Millisecond + "_" + fileName;
            }

            postedFile.SaveAs(HttpContext.Current.Server.MapPath(FilePath) + Path.DirectorySeparatorChar + fileName);

            if (FilePath != "/")
            {
                return VirtualPathUtility.AppendTrailingSlash(FilePath) + fileName;
            }
            else
            {
                return fileName;
            }
        }

        /// <summary>
        /// Uploads the specified posted file.
        /// Component checks file's validation first.
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="subFolder">like "/subimages"</param>
        /// <returns></returns>
        public static string Upload(HttpPostedFile postedFile, string subFolder)
        {
            string fileName = Path.GetFileName(postedFile.FileName);
            //Xử lý lỗi tên file dài quá 240 ký tự
            if (fileName.Length >= 100)
            {
                fileName = fileName.Substring(fileName.Length - 100);
            }

            CheckValid(postedFile);

            //Kiem tra su ton tai cua file, neu ton tai se doi ten file
            var filePath = HttpContext.Current.Server.MapPath(FilePath + subFolder) + Path.DirectorySeparatorChar + fileName;
            if (File.Exists(filePath))
            {
                fileName = DateTime.Now.Ticks + "_" + (new Random()).Next(10000000) + "_" + fileName;
            }

            if (!Directory.Exists(HttpContext.Current.Server.MapPath(FilePath + subFolder)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(FilePath + subFolder));
            }

            if (postedFile.ContentLength > 0)
            {
                postedFile.SaveAs(VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Server.MapPath(FilePath + subFolder)) + fileName);
            }
            else
            {
                return string.Empty;
            }

            if (FilePath != "/")
            {
                return VirtualPathUtility.AppendTrailingSlash(FilePath + subFolder) + fileName;
            }
            else
            {
                return VirtualPathUtility.AppendTrailingSlash(subFolder) + fileName;
            }
        }

        public static string Upload(HttpPostedFile postedFile, string subFolder, string text, Font font, Brush brushes, StringFormat sf)
        {
            if (String.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("Text field is empty.");
            }

            string fileName = Path.GetFileName(postedFile.FileName);
            //Xử lý lỗi tên file dài quá 240 ký tự
            if (fileName.Length >= 100)
            {
                fileName = fileName.Substring(fileName.Length - 100);
            }
            CheckValid(postedFile);

            //Kiem tra su ton tai cua file, neu ton tai se doi ten file
            var filePath = HttpContext.Current.Server.MapPath(FilePath + subFolder) + Path.DirectorySeparatorChar + fileName;
            if (File.Exists(filePath))
            {
                fileName = DateTime.Now.Ticks + "_" + (new Random()).Next(10000000) + "_" + fileName;
            }

            if (!Directory.Exists(HttpContext.Current.Server.MapPath(FilePath + subFolder)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(FilePath + subFolder));
            }

            if (postedFile.ContentLength > 0)
            {
                postedFile.SaveAs(VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Server.MapPath(FilePath + subFolder)) + fileName);

                //Drawstring on image
                Bitmap bitMapImage = new Bitmap(HttpContext.Current.Server.MapPath(FilePath + subFolder) + Path.DirectorySeparatorChar + fileName);
                using (Graphics graphicImage = Graphics.FromImage(bitMapImage))
                {
                    //Smooth graphics is nice.
                    graphicImage.SmoothingMode = SmoothingMode.AntiAlias;

                    //Write your text.
                    fileName = DateTime.Now.Ticks + "_" + (new Random()).Next(10000000) + "_" + fileName;
                    graphicImage.DrawString(text, font, brushes, new Rectangle(0, 0, bitMapImage.Width, bitMapImage.Height), sf);

                    if (fileName.ToLower().EndsWith(".png"))
                    {
                        bitMapImage.MakeTransparent();
                        bitMapImage.Save(HttpContext.Current.Server.MapPath(FilePath + subFolder) + Path.DirectorySeparatorChar + fileName, ImageFormat.Png);
                    }
                    else
                    {
                        bitMapImage.Save(HttpContext.Current.Server.MapPath(FilePath + subFolder) + Path.DirectorySeparatorChar + fileName);
                    }

                    bitMapImage.Dispose();
                }//using
            }
            else
            {
                return string.Empty;
            }

            if (FilePath != "/")
            {
                return VirtualPathUtility.AppendTrailingSlash(FilePath + subFolder) + fileName;
            }
            else
            {
                return VirtualPathUtility.AppendTrailingSlash(subFolder) + fileName;
            }
        }

        public static string Upload(HttpPostedFile postedFile, string subFolder, string text, Font font, Brush brushes, PointF point)
        {
            if (String.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("Text field is empty.");
            }

            string fileName = Path.GetFileName(postedFile.FileName);
            //Xử lý lỗi tên file dài quá 240 ký tự
            if (fileName.Length >= 100)
            {
                fileName = fileName.Substring(fileName.Length - 100);
            }
            CheckValid(postedFile);

            //Kiem tra su ton tai cua file, neu ton tai se doi ten file
            var filePath = HttpContext.Current.Server.MapPath(FilePath + subFolder) + Path.DirectorySeparatorChar + fileName;
            if (File.Exists(filePath))
            {
                fileName = DateTime.Now.Ticks + "_" + (new Random()).Next(10000000) + "_" + fileName;
            }

            if (!Directory.Exists(HttpContext.Current.Server.MapPath(FilePath + subFolder)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(FilePath + subFolder));
            }

            if (postedFile.ContentLength > 0)
            {
                postedFile.SaveAs(VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Server.MapPath(FilePath + subFolder)) + fileName);

                //Drawstring on image
                Bitmap bitMapImage = new Bitmap(HttpContext.Current.Server.MapPath(FilePath + subFolder) + Path.DirectorySeparatorChar + fileName);
                using (Graphics graphicImage = Graphics.FromImage(bitMapImage))
                {
                    //Smooth graphics is nice.
                    graphicImage.SmoothingMode = SmoothingMode.AntiAlias;

                    //Write your text.
                    fileName = DateTime.Now.Ticks + "_" + (new Random()).Next(10000000) + "_" + fileName;
                    graphicImage.DrawString(text, font, brushes, point);

                    if (fileName.ToLower().EndsWith(".png"))
                    {
                        bitMapImage.MakeTransparent();
                        bitMapImage.Save(HttpContext.Current.Server.MapPath(FilePath + subFolder) + Path.DirectorySeparatorChar + fileName, ImageFormat.Png);
                    }
                    else
                    {
                        bitMapImage.Save(HttpContext.Current.Server.MapPath(FilePath + subFolder) + Path.DirectorySeparatorChar + fileName);
                    }

                    bitMapImage.Dispose();
                }//using
            }
            else
            {
                return string.Empty;
            }

            if (FilePath != "/")
            {
                return VirtualPathUtility.AppendTrailingSlash(FilePath + subFolder) + fileName;
            }
            else
            {
                return VirtualPathUtility.AppendTrailingSlash(subFolder) + fileName;
            }
        }

        /// <summary>
        /// Checks the valid.
        /// </summary>
        /// <param name="postedFile">The posted file.</param>
        public static void CheckValid(HttpPostedFile postedFile)
        {
            //Kiem tra tinh hop le ve dinh dang file
            string extension = Path.GetExtension(postedFile.FileName).ToLower();

            if (!ValidExtensions.Exists(e => e.EndsWith(extension)))
            {
                throw new InvalidFileException(String.Format("Bạn chỉ được phép upload các định dạng file sau: {0}", String.Join(",", ValidExtensions.ToArray())));
            }

            //Kiem tra tinh hop le ve dung luong file
            if (!CheckValidCapacity(postedFile.ContentLength))
            {
                throw new InvalidFileException("Dung lượng file lớn hơn " + Capacity + " MB");
            }
        }

        private static bool CheckValidCapacity(int contentLength)
        {
            if ((contentLength / (1024 * 1024)) > Capacity)
                return false;

            return true;
        }
    }
}
