using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace SAT.SERVICES.Services
{
    public class ExcelExporterService<T> where T : new()
    {
        private string FilePath;
        private string Extension;
        private T Entity;
        private readonly int STARTING_INDEX = 1;

        public ExcelExporterService()
        {
            this.Entity = new T();
            this.Extension = ".xlsx";
            this.FilePath = Path.Combine(Path.GetTempPath(),
                                            String.Concat(Entity.GetType().Name,
                                            String.Join("", DateTime.Now.ToString().Where(d => Char.IsDigit(d))), this.Extension));
        }
        private bool ValidateHeader(string header)
        {
            if (header.StartsWith("Cod")) return false;
            if (IgnoredColumns.Contains(header)) return false;

            return true;
        }

        private void FormatSheets(IXLWorksheet mainSheet)
        {
            mainSheet.FirstRow().CellsUsed().ToList().ForEach(c =>
            {
                if (!ValidateHeader(c.Value.ToString()))
                    mainSheet.Column(c.WorksheetColumn().ColumnNumber()).Delete();
            });
        }

        public IActionResult WriteToExcel(List<T> rows)
        {
            XLWorkbook workbook = new XLWorkbook();
            workbook.Worksheets.Add("Main Sheet");
            IXLWorksheet mainSheet = workbook.Worksheets.First();

            WriteDataContent(rows, mainSheet);
            WriteHeaders(this.Entity, mainSheet);
            FormatSheets(mainSheet);

            mainSheet.Columns().AdjustToContents();
            mainSheet.RangeUsed().Style.Border.SetOutsideBorder(XLBorderStyleValues.Medium);
            mainSheet.RangeUsed().Style.Border.SetInsideBorder(XLBorderStyleValues.Medium);
            SaveFile(workbook);

            return GenerateFile();
        }

        private void SaveFile(XLWorkbook workbook)
        {
            workbook.SaveAs(this.FilePath);
        }

        private FileContentResult GenerateFile()
        {
            byte[] file = File.ReadAllBytes(this.FilePath);
            return new FileContentResult(file, "application/octet-stream");
        }

        private void WriteHeaders(T entity, IXLWorksheet mainSheet)
        {
            int cellIndex = STARTING_INDEX;
            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                mainSheet.Cell(STARTING_INDEX, cellIndex).Value = prop.Name;
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
        }

        private void WriteDataContent(List<T> rows, IXLWorksheet mainSheet)
        {
            int rowIndex = STARTING_INDEX + 1;
            rows.ForEach(r =>
            {
                int cellIndex = STARTING_INDEX;
                foreach (PropertyInfo prop in r.GetType().GetProperties())
                {
                    string value = "";

                    if (!prop.PropertyType.IsPrimitive)
                    {
                        value = prop.PropertyType.GetProperties()
                                                 .FirstOrDefault(p => p.Name.StartsWith("Nom"))?
                                                 .GetValue(r, null)?
                                                 .ToString();

                        if (string.IsNullOrEmpty(value))
                            value = prop.PropertyType.GetProperties()
                                                 .FirstOrDefault(p => p.Name.StartsWith("Cod"))?
                                                 .GetValue(r, null)?
                                                 .ToString();
                    }
                    else
                    {
                        value = prop.GetValue(r, null)?.ToString();
                    }

                    mainSheet.Cell(rowIndex, cellIndex).Value = value;
                    cellIndex++;
                }
                rowIndex++;
            });
        }

        private readonly string[] IgnoredColumns =
        {
            "DataHoraManut",
            "DataHoraCad",
            "DataHoraAtualizacaoValor",
            "IsValorAtualizado",
            "DataAtualizacao",
            "DataIntegracaoLogix"
        };


    }
}