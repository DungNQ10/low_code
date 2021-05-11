using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lowcode.Api.Helpers
{
    public class ExcelHelper
    {
        public string ConvertToXls(string xlsxPath)
        {
            var oldWorkbook = new XSSFWorkbook(new FileStream(xlsxPath, FileMode.Open));
            var oldWorkSheet = oldWorkbook.GetSheetAt(0);
            var newExcelPath = xlsxPath.Replace("xls", "xlsx");
            using (var fileStream = new FileStream(newExcelPath, FileMode.Create))
            {
                var newWorkBook = new HSSFWorkbook();
                var newWorkSheet = newWorkBook.CreateSheet("Sheet1");

                foreach (HSSFRow oldRow in oldWorkSheet)
                {
                    var newRow = newWorkSheet.CreateRow(oldRow.RowNum);

                    for (int ii = oldRow.FirstCellNum; ii < oldRow.LastCellNum; ii++)
                    {
                        var newCell = newRow.CreateCell(ii);
                        newCell = oldRow.Cells[ii];
                    }
                }

                newWorkBook.Write(fileStream);
            }

            return newExcelPath;
        }
    }
}
