using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace SAT.SERVICES.Services
{
    public class ExcelService<T> where T : class
    {
        private XLWorkbook Workbook { get; set; }
        public int STARTING_INDEX = 1;
        public string FilePath { get; set; }

        public ExcelService()
        {
            FilePath = GenerateFilePath();
        }

        public IActionResult ExportExcel(List<List<object>> lista) //List<object> obj, List<object> obj2 = null
        {
            Workbook = new XLWorkbook();

            lista.ForEach(obj =>
            {
                WriteSheet(obj);
            });

            SaveFile(FilePath);
            return GenerateFile();
        }

        private IActionResult GenerateFile()
        {
            byte[] file = File.ReadAllBytes(FilePath);
            return new FileContentResult(file, "application/octet-stream");
        }

        private void SaveFile(string filePath)
        {
            Workbook.SaveAs(filePath);
        }

        private IXLWorksheet WriteSheet(List<object> data)
        {
            IXLWorksheet sheet = null;
            string entityName = data.FirstOrDefault().GetType().Name;

            if (!Workbook.TryGetWorksheet(entityName, out sheet))
            {
                sheet = Workbook.Worksheets.Add(entityName);
                WriteHeaders(data.FirstOrDefault(), sheet);
            }

            sheet.Cell(2, 1).Value = data;

            FormatSheet(sheet.LastRowUsed().Cells(), sheet);
            return sheet;
        }

        private void FormatSheet(IXLCells row, IXLWorksheet sheet)
        {
            row.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            sheet.Columns().AdjustToContents();
            sheet.Rows().AdjustToContents();
        }

        private void WriteHeaders(object entity, IXLWorksheet mainSheet)
        {
            int cellIndex = 1;
            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {

                mainSheet.Cell(1, cellIndex).Value = prop.Name;
                cellIndex++;
            }
            FormatHeader(mainSheet);
        }

        private void FormatHeader(IXLWorksheet mainSheet)
        {
            IXLCells headerRowCells = mainSheet.FirstRow().Cells();

            headerRowCells.Style.Font.SetBold();
            headerRowCells.Style.Font.FontColor = XLColor.FromHtml("#fff");
            headerRowCells.Style.Fill.BackgroundColor = XLColor.FromHtml("#1c3a70");
            headerRowCells.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        }

        private string GenerateFilePath()
        {
            return Path.Combine(Path.GetTempPath(),
                        String.Concat(typeof(T).Name,
                        String.Join("", DateTime.Now.ToString().Where(d => Char.IsDigit(d))), ".xlsx"));
        }
    }
}