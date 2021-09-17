using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace SAT.SERVICES.Services
{
    public class BaseExcelService<T> where T : new()
    {
        private readonly int STARTING_INDEX = 1;

        protected List<string> IgnoredProperties { get; set; }
        protected List<string> ComplexProperties { get; set; }
        protected Dictionary<string, string> SimpleProperties { get; set; }
        private XLWorkbook Workbook { get; set; }

        public IActionResult CreateWorkbook(List<T> data)
        {
            string filePath = GenerateFilePath();
            Workbook = new XLWorkbook();

            WriteSheet(data);
            SaveFile(filePath);
            return GenerateFile(filePath);
        }

        private void SaveFile(string filePath)
        {
            Workbook.SaveAs(filePath);
        }

        private FileContentResult GenerateFile(string path)
        {
            byte[] file = File.ReadAllBytes(path);
            return new FileContentResult(file, "application/octet-stream");
        }

        private IXLWorksheet WriteSheet(List<T> data)
        {
            IXLWorksheet sheet = null;
            string entityName = typeof(T).Name;
            if (!Workbook.TryGetWorksheet(entityName, out sheet))
            {
                sheet = Workbook.Worksheets.Add(entityName);
                WriteHeaders(new T(), sheet);
            }

            WriteDataContent(data, sheet);

            sheet.Columns().AdjustToContents();
            return sheet;
        }

        private void WriteHeaders(T entity, IXLWorksheet mainSheet)
        {
            int cellIndex = STARTING_INDEX;
            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                if (!IgnoredProperties.Contains(prop.Name) && !ComplexProperties.Contains(prop.Name))
                {
                    mainSheet.Cell(STARTING_INDEX, cellIndex).Value = prop.Name;
                    cellIndex++;
                }
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

        private void FormatRow(IXLCells row)
        {
            row.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        }

        private void FormatSheet(IXLWorksheet sheet)
        {
            sheet.Columns().AdjustToContents();
            sheet.Rows().AdjustToContents();
        }

        private void WriteDataContent(List<T> data, IXLWorksheet sheet)
        {
            int rowIndex = sheet.RowsUsed().Count() + 1;
            data.ForEach(d =>
            {
                int cellIndex = STARTING_INDEX;
                foreach (PropertyInfo prop in d.GetType().GetProperties())
                {
                    string cellValue = "";

                    if (IgnoredProperties.Contains(prop.Name))
                    {
                        continue;
                    }
                    else if (SimpleProperties.ContainsKey(prop.Name))
                    {
                        string mainprop = SimpleProperties.First(sp => sp.Key == prop.PropertyType.Name).Value;

                        var obj = prop.GetValue(d);
                        if (obj != null)
                            cellValue = prop.PropertyType.GetProperties()?
                                            .First(p => p.Name == mainprop)?
                                            .GetValue(obj)?.ToString();
                    }
                    else if (ComplexProperties.Contains(prop.Name))
                    {
                        // TO DO
                        continue;
                    }
                    else
                    {
                        cellValue = prop.GetValue(d)?.ToString();
                    }

                    if (!string.IsNullOrEmpty(cellValue))
                        sheet.Cell(rowIndex, cellIndex).Value = cellValue;

                    cellIndex++;
                }
                FormatRow(sheet.LastRowUsed().Cells());
                rowIndex++;
            });
        }

        private string GenerateFilePath()
        {
            return Path.Combine(Path.GetTempPath(),
                        String.Concat(typeof(T).Name,
                        String.Join("", DateTime.Now.ToString().Where(d => Char.IsDigit(d))), ".xlsx"));
        }
    }
}