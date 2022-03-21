using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.ViewModels;
using SAT.INFRA.Interfaces;

namespace SAT.SERVICES.Services
{
	public partial class ExportacaoService : IExportacaoService
	{
		private XLWorkbook Workbook { get; set; }
		public string FilePath { get; set; }
		public int ExportacaoTipo { get; set; }
		private IOrdemServicoRepository _osRepo;
		private IEquipamentoContratoRepository _ecRepo;

		public ExportacaoService(IOrdemServicoRepository osRepo, IEquipamentoContratoRepository ecRepo)
		{
			_osRepo = osRepo;
			_ecRepo = ecRepo;
			FilePath = GenerateFilePath();
		}

		public dynamic Exportar(dynamic parameters)
		{
			ExportacaoTipo = parameters.ExportType;

			switch (parameters.ExportFormat)
			{
				case (int)ExportacaoFormatoEnum.EXCEL:

					return ExportExcel(parameters);

				default:

					return null;
			}
		}

		public IActionResult ExportExcel(dynamic parameters)
		{
			Workbook = new XLWorkbook();

			switch (ExportacaoTipo)
			{
				case 1:
					GerarPlanilhaOrdemServico(parameters);
					break;

				case 2:
					GerarPlanilhaEquipamentoContrato(parameters);
					break;
				default:
					break;
			}

			foreach (var ws in Workbook.Worksheets)
			{
				FormatSheet(ws.CellsUsed(),ws);
			}

			Workbook.SaveAs(FilePath);

			byte[] file = File.ReadAllBytes(FilePath);
			return new FileContentResult(file, "application/octet-stream");
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
						String.Concat("zexcel",
						String.Join("", DateTime.Now.ToString().Where(d => Char.IsDigit(d))), ".xlsx"));
		}
	}
}