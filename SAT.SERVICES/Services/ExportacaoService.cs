using System;
using System.Linq;
using System.Reflection;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;
using SAT.INFRA.Interfaces;

namespace SAT.SERVICES.Services
{
	public partial class ExportacaoService : IExportacaoService
	{
		private XLWorkbook Workbook { get; set; }
		public string FilePath { get; set; }
		private readonly IOrdemServicoRepository _osRepo;
		private readonly IEquipamentoContratoRepository _ecRepo;
        private readonly IAcaoRepository _acaoRepo;
		private readonly IAcaoComponenteRepository _acaoComponenteRepo;
		private readonly IAutorizadaRepository _autorizadaRepo;
		private readonly ITecnicoRepository _tecnicoRepo;
		private readonly ICidadeRepository _cidadeRepo;
		private readonly IClienteRepository _clienteRepo;
		private readonly IClientePecaRepository _clientePecaRepo;
		private readonly IClientePecaGenericaRepository _clientePecaGenericaRepo;
		private readonly IClienteBancadaRepository _clienteBancadaRepo;

        public ExportacaoService(
			IOrdemServicoRepository osRepo, 
			IEquipamentoContratoRepository ecRepo,
			IAcaoRepository acaoRepo,
			IAcaoComponenteRepository acaoComponenteRepo,
			IAutorizadaRepository autorizadaRepo,
			ITecnicoRepository tecnicoRepo,
			ICidadeRepository cidadeRepo,
			IClienteRepository clienteRepo,
			IClientePecaRepository clientePecaRepo,
			IClientePecaGenericaRepository clientePecaGenericaRepo,
			IClienteBancadaRepository clienteBancadaRepo
		)
		{
			_osRepo = osRepo;
			_ecRepo = ecRepo;
			_acaoRepo = acaoRepo;
			_acaoComponenteRepo = acaoComponenteRepo;
			_autorizadaRepo = autorizadaRepo;
			_cidadeRepo = cidadeRepo;
			_tecnicoRepo = tecnicoRepo;
			_clienteRepo = clienteRepo;
			_clientePecaRepo = clientePecaRepo;
			_clientePecaGenericaRepo = clientePecaGenericaRepo;
			_clienteBancadaRepo = clienteBancadaRepo;
			FilePath = GenerateFilePath();
		}

		public dynamic Exportar(dynamic parameters, ExportacaoFormatoEnum formato, ExportacaoTipoEnum tipo)
		{
			switch (formato)
			{
				case ExportacaoFormatoEnum.EXCEL:
					return ExportExcel(parameters, tipo);

				default:
					return null;
			}
		}

		public IActionResult ExportExcel(dynamic parameters, ExportacaoTipoEnum tipo)
		{
			Workbook = new XLWorkbook();

			switch (tipo)
			{
				case ExportacaoTipoEnum.ORDEM_SERVICO:
					GerarPlanilhaOrdemServico(parameters);
					break;

				case ExportacaoTipoEnum.EQUIPAMENTO_CONTRATO:
					GerarPlanilhaEquipamentoContrato(parameters);
					break;

				case ExportacaoTipoEnum.ACAO:
					GerarPlanilhaAcao(parameters);
					break;
				
				case ExportacaoTipoEnum.ACAO_COMPONENTE:
					GerarPlanilhaAcaoComponente(parameters);
					break;

				case ExportacaoTipoEnum.AUTORIZADA:
					GerarPlanilhaAutorizada(parameters);
					break;		

				case ExportacaoTipoEnum.CIDADE:
					GerarPlanilhaCidade(parameters);
					break;		

				case ExportacaoTipoEnum.TECNICO:
					GerarPlanilhaTecnico(parameters);
					break;					

				case ExportacaoTipoEnum.CLIENTE:
					GerarPlanilhaCliente(parameters);
					break;				
				
				case ExportacaoTipoEnum.CLIENTEPECA:
					GerarPlanilhaClientePeca(parameters);
					break;										
				
				case ExportacaoTipoEnum.CLIENTEPECAGENERICA:
					GerarPlanilhaClientePecaGenerica(parameters);
					break;

				case ExportacaoTipoEnum.CLIENTEBANCADA:
					GerarPlanilhaClienteBancada(parameters);
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
			sheet.RangeUsed()?.SetAutoFilter();
			sheet.Columns()?.AdjustToContents();
			sheet.Rows()?.AdjustToContents();
		}

		private void WriteHeaders(object entity, IXLWorksheet mainSheet)
		{
			try
			{
				int cellIndex = 1;

				if (entity == null)
					return;

				var properties = entity.GetType()?.GetProperties();

				foreach (PropertyInfo prop in properties)
				{
					if (prop != null) {
						mainSheet.Cell(1, cellIndex).Value = prop?.Name;
						cellIndex++;
					}
				}
				FormatHeader(mainSheet);	
			}
			catch (Exception ex)
			{
				throw new Exception("Ocorreu um erro ao exportar", ex);
			}
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