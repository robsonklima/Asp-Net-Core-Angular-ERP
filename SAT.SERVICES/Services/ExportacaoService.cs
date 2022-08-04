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
        private readonly IContratoRepository _contratoRepo;
        private readonly IDefeitoRepository _defeitoRepo;
        private readonly IDefeitoComponenteRepository _defeitoComponenteRepo;
        private readonly IEquipamentoModuloRepository _equipamentoModuloRepo;
        private readonly IEquipamentoRepository _equipamentoRepo;
        private readonly IGrupoEquipamentoRepository _grupoEquipamentoRepo;
        private readonly ITipoEquipamentoRepository _tipoEquipamentoRepo;
        private readonly IFeriadoRepository _feriadoRepo;
        private readonly IFerramentaTecnicoRepository _ferramentaTecnicoRepo;
        private readonly IFormaPagamentoRepository _formaPagamentoRepo;
        private readonly ILiderTecnicoRepository _liderTecnicoRepo;
        private readonly ILocalAtendimentoRepository _localAtendimentoRepo;
        private readonly IPecaRepository _pecaRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IRegiaoRepository _regiaoRepo;
        private readonly IRegiaoAutorizadaRepository _regiaoAutorizadaRepo;
        private readonly IFilialRepository _filialRepo;

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
            IClienteBancadaRepository clienteBancadaRepo,
            IContratoRepository contratoRepo,
            IDefeitoRepository defeitoRepo,
            IEquipamentoRepository equipamentoRepo,
            IGrupoEquipamentoRepository grupoEquipamentoRepo,
            ITipoEquipamentoRepository tipoEquipamentoRepo,
            IDefeitoComponenteRepository defeitoComponenteRepo,
            IEquipamentoModuloRepository equipamentoModuloRepo,
            IFeriadoRepository feriadoRepo,
            IFerramentaTecnicoRepository ferramentaTecnicoRepo,
            IFormaPagamentoRepository formaPagamentoRepo,
        	ILiderTecnicoRepository liderTecnicoRepo,
        	ILocalAtendimentoRepository localAtendimentoRepo,
        	IPecaRepository pecaRepo,
        	IUsuarioRepository usuarioRepo,
        	IRegiaoRepository regiaoRepo,
        	IRegiaoAutorizadaRepository regiaoAutorizadaRepo,
            IFilialRepository filialRepo
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
			_contratoRepo = contratoRepo;
			_defeitoRepo = defeitoRepo;
			_equipamentoRepo = equipamentoRepo;
			_grupoEquipamentoRepo = grupoEquipamentoRepo;
			_tipoEquipamentoRepo = tipoEquipamentoRepo;
			_defeitoComponenteRepo = defeitoComponenteRepo;
			_equipamentoModuloRepo = equipamentoModuloRepo;
			_feriadoRepo = feriadoRepo;
			_ferramentaTecnicoRepo = ferramentaTecnicoRepo;
			_formaPagamentoRepo = formaPagamentoRepo;
			_liderTecnicoRepo = liderTecnicoRepo;
			_localAtendimentoRepo = localAtendimentoRepo;
			_pecaRepo = pecaRepo;
			_usuarioRepo = usuarioRepo;
			_regiaoRepo = regiaoRepo;
			_regiaoAutorizadaRepo = regiaoAutorizadaRepo;
			_filialRepo = filialRepo;
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

            case ExportacaoTipoEnum.CONTRATO:
                GerarPlanilhaContrato(parameters);
                break;

            case ExportacaoTipoEnum.DEFEITO:
                GerarPlanilhaDefeito(parameters);
                break;

            case ExportacaoTipoEnum.EQUIPAMENTO:
                GerarPlanilhaEquipamento(parameters);
                break;

            case ExportacaoTipoEnum.GRUPOEQUIPAMENTO:
                GerarPlanilhaGrupoEquipamento(parameters);
                break;

            case ExportacaoTipoEnum.TIPOEQUIPAMENTO:
                GerarPlanilhaTipoEquipamento(parameters);
                break;

            case ExportacaoTipoEnum.DEFEITOCOMPONENTE:
                GerarPlanilhaDefeitoComponente(parameters);
                break;

            case ExportacaoTipoEnum.EQUIPAMENTOMODULO:
                GerarPlanilhaEquipamentoModulo(parameters);
                break;

            case ExportacaoTipoEnum.FERIADO:
                GerarPlanilhaFeriado(parameters);
                break;

            case ExportacaoTipoEnum.FERRAMENTATECNICO:
                GerarPlanilhaFerramentaTecnico(parameters);
                break;

            case ExportacaoTipoEnum.FILIAL:
                GerarPlanilhaFilial(parameters);
                break;

            case ExportacaoTipoEnum.FORMAPAGAMENTO:
                GerarPlanilhaFormaPagamento(parameters);
                break;

            case ExportacaoTipoEnum.LIDERTECNICO:
                GerarPlanilhaLiderTecnico(parameters);
                break;

            case ExportacaoTipoEnum.LOCALATENDIMENTO:
                GerarPlanilhaLocalAtendimento(parameters);
                break;

            case ExportacaoTipoEnum.PECA:
                GerarPlanilhaPeca(parameters);
                break;

            default:
                break;
        }

        foreach (var ws in Workbook.Worksheets)
        {
            FormatSheet(ws.CellsUsed(), ws);
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
                if (prop != null)
                {
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