using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;
using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Tables;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;


namespace Lowcode.Framework.Utils
{
    public class DocxUtils
    {
        public static string GetDocxInnerText(string docxFilepath, string extractionFolder)
        {
            if (Directory.Exists(extractionFolder))
                Directory.Delete(extractionFolder, true);

            ZipFile.ExtractToDirectory(docxFilepath, extractionFolder);
            string xmlFilepath = extractionFolder + "\\word\\document.xml";

            using (StreamReader sr = new StreamReader(xmlFilepath))
            {
                // Read the stream to a string, and write the string to the console.
                String content = sr.ReadToEnd();

                return content;
            }
        }

        public static void DocXmlToDocx(string document, string docXml, string docxExtractFolder)
        {
            var xmlPath = $"{docxExtractFolder}\\word\\document.xml";
            using (StreamWriter stream = new StreamWriter(xmlPath))
            {
                stream.Write(docXml);
            }
            ZipFile.CreateFromDirectory(docxExtractFolder, document);

        }

        public static void DocxToPdf()
        {

            //var licenseKey = @"N9Q9EUUVTICI";

            //Spire.License.LicenseProvider.SetLicenseKey(licenseKey);
            ////Spire.License.LicenseProvider.LoadLicense();
            //Document doc = new Document();
            //doc.LoadFromFile(@"C:\Working\Outsource\WGD\THUVIEN\doc\20200224_WGD-THUVIEN-Yeu cau chuc nang.docx");
            //doc.SaveToFile(@"C:\Working\Outsource\WGD\THUVIEN\doc\20200224_WGD-THUVIEN-Yeu cau chuc nang.pdf", FileFormat.PDF);
            //#region DocConvertToPdf

            //var locationOfLibreOfficeSoffice = @"C:\Program Files\LibreOffice\program\soffice.exe";
            //var test = new ReportGenerator(locationOfLibreOfficeSoffice);
            //test.Convert(@"C:\Working\Outsource\WGD\THUVIEN\doc\20200224_WGD-THUVIEN-Yeu cau chuc nang.docx", @"C:\Working\Outsource\WGD\THUVIEN\doc\20200224_WGD-THUVIEN-Yeu cau chuc nang.pdf");
            //test.Convert(@"C:\Working\Outsource\WGD\THUVIEN\doc\Facilites list.xlsx", @"C:\Working\Outsource\WGD\THUVIEN\doc\Facilites list.pdf"); 
            //#endregion

            new Aspose.Words.License().SetLicense(AsposeLicense.GetLStream());
            var doc = new Document(@"C:\Working\Outsource\WGD\THUVIEN\doc\20200224_WGD-THUVIEN-Yeu cau chuc nang.docx");
            doc.Save(@"C:\Working\Outsource\WGD\THUVIEN\doc\20200224_WGD-THUVIEN-Yeu cau chuc nang.pdf",
                SaveFormat.Pdf);

        }


        public static void RegisterAsposeWord()
        {
            new Aspose.Words.License().SetLicense(AsposeLicense.GetLStream());
        }


    }
}

