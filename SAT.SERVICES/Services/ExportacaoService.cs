using System;
using System.Reflection;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;

namespace SAT.SERVICES.Services
{
    public partial class ExportacaoService : IExportacaoService
    {
        private XLWorkbook Workbook { get; set; }
        public string FilePath { get; set; }
        private readonly IEmailService _emaiLService;
        private readonly IOrdemServicoRepository _osRepo;
        private readonly IEquipamentoContratoRepository _ecRepo;
        private readonly IAcaoRepository _acaoRepo;
        private readonly IFotoRepository _fotoRepo;
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
        private readonly IFerramentaTecnicoRepository _ferramentaTecnicoRepo;
        private readonly IFormaPagamentoRepository _formaPagamentoRepo;
        private readonly ILiderTecnicoRepository _liderTecnicoRepo;
        private readonly ILocalAtendimentoRepository _localAtendimentoRepo;
        private readonly IPecaRepository _pecaRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IRegiaoRepository _regiaoRepo;
        private readonly IOrcamentoRepository _orcamentoRepo;
        private readonly IRegiaoAutorizadaRepository _regiaoAutorizadaRepo;
        private readonly IFilialRepository _filialRepo;
        private readonly IAuditoriaRepository _auditoriaRepo;
        private readonly IDespesaPeriodoTecnicoRepository _despesaPeriodoTecnicoRepo;
        private readonly IDespesaService _despesaService;
        private readonly IDespesaAdiantamentoRepository _despesaAdiantamentoRepo;
        private readonly IDespesaAdiantamentoPeriodoRepository _despesaAdiantamentoPeriodoRepo;
        private readonly IORRepository _orRepo;
        private readonly IORItemRepository _orItemRepo;
        private readonly IRelatorioAtendimentoRepository _relatorioAtendimentoRepo;
        private readonly ITicketLogPedidoCreditoRepository _ticketLogPedidoCreditoRepo;
        private readonly IORCheckListRepository _orCheckListRepo;
        private readonly IDespesaCartaoCombustivelRepository _despesaCartaoCombustivelRepo;
        private readonly ITicketLogTransacaoRepository _ticketLogTransacaoRepo;
        private readonly ITicketRepository _ticketRepo;
        private readonly ILaudoRepository _laudoRepo;
        private readonly IInstalacaoRepository _instalacaoRepo;
        private readonly IInstalacaoService _instalacaoSrv;
        private readonly IPontoUsuarioRepository _pontoUsuarioRepo;
        private readonly IInstalacaoPleitoRepository _instalacaoPleitoRepo;
        private readonly IInstalacaoPleitoInstalRepository _instalacaoPleitoInstalRepo;
        private readonly IOsBancadaPecasOrcamentoRepository _osBancadaPecasOrcamentoRepo;
        private readonly IOSBancadaPecasRepository _osBancadaPecasRepo;
        private readonly IOrcamentoPecasEspecRepository _orcamentoPecasEspecRepo;
        private readonly IUsuarioService _usuarioService;
        private readonly IHttpContextAccessor _contextAcecssor;
        private readonly IInstalacaoPagtoInstalRepository _instalPagtoIntalRepo;
        private readonly IEquipamentoContratoService _ecSvc;

        public ExportacaoService(
            IEmailService emaiLService,
            IOrdemServicoRepository osRepo,
            IEquipamentoContratoRepository ecRepo,
            IAcaoRepository acaoRepo,
            IFotoRepository fotoRepo,
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
            IFerramentaTecnicoRepository ferramentaTecnicoRepo,
            IFormaPagamentoRepository formaPagamentoRepo,
            ILiderTecnicoRepository liderTecnicoRepo,
            ILocalAtendimentoRepository localAtendimentoRepo,
            IPecaRepository pecaRepo,
            IUsuarioRepository usuarioRepo,
            IDespesaPeriodoTecnicoRepository despesaPeriodoTecnicoRepo,
            IRegiaoRepository regiaoRepo,
            IOrcamentoRepository orcamentoRepo,
            IRegiaoAutorizadaRepository regiaoAutorizadaRepo,
            IFilialRepository filialRepo,
            IDespesaService despesaService,
            IAuditoriaRepository auditoriaRepo,
            IDespesaAdiantamentoRepository despesaAdiantamentoRepo,
            IDespesaAdiantamentoPeriodoRepository despesaAdiantamentoPeriodoRepo,
            IORRepository orRepo,
            IORItemRepository orItemRepo,
            IRelatorioAtendimentoRepository relatorioAtendimentoRepo,
            ITicketLogPedidoCreditoRepository ticketLogPedidoCreditoRepo,
            IORCheckListRepository orCheckListRepo,
            IDespesaCartaoCombustivelRepository despesaCartaoCombustivelRepo,
            ITicketLogTransacaoRepository ticketLogTransacaoRepo,
            ITicketRepository ticketRepo,
            ILaudoRepository laudoRepo,
            IPontoUsuarioRepository pontoUsuarioRepo,
            IInstalacaoRepository instalacaoRepo,
            IInstalacaoService instalacaoSrv,
            IInstalacaoPleitoRepository instalacaoPleitoRepo,
            IInstalacaoPleitoInstalRepository instalacaoPleitoInstalRepo,
            IOsBancadaPecasOrcamentoRepository osBancadaPecasOrcamentoRepo,
            IOSBancadaPecasRepository osBancadaPecasRepo,
            IOrcamentoPecasEspecRepository orcamentoPecasEspecRepo,
            IHttpContextAccessor httpContextAccessor,
            IUsuarioService usuarioService,
            IInstalacaoPagtoInstalRepository instalPagtoIntalRepo,
            IEquipamentoContratoService equipamentoContratoSvc
        )
        {
            _emaiLService = emaiLService;
            _osRepo = osRepo;
            _ecRepo = ecRepo;
            _acaoRepo = acaoRepo;
            _fotoRepo = fotoRepo;
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
            _ferramentaTecnicoRepo = ferramentaTecnicoRepo;
            _formaPagamentoRepo = formaPagamentoRepo;
            _liderTecnicoRepo = liderTecnicoRepo;
            _localAtendimentoRepo = localAtendimentoRepo;
            _pecaRepo = pecaRepo;
            _usuarioRepo = usuarioRepo;
            _regiaoRepo = regiaoRepo;
            _orcamentoRepo = orcamentoRepo;
            _regiaoAutorizadaRepo = regiaoAutorizadaRepo;
            _filialRepo = filialRepo;
            _auditoriaRepo = auditoriaRepo;
            _despesaPeriodoTecnicoRepo = despesaPeriodoTecnicoRepo;
            _despesaService = despesaService;
            _despesaAdiantamentoRepo = despesaAdiantamentoRepo;
            _despesaAdiantamentoPeriodoRepo = despesaAdiantamentoPeriodoRepo;
            _orRepo = orRepo;
            _orItemRepo = orItemRepo;
            _relatorioAtendimentoRepo = relatorioAtendimentoRepo;
            _ticketLogPedidoCreditoRepo = ticketLogPedidoCreditoRepo;
            _orCheckListRepo = orCheckListRepo;
            _despesaCartaoCombustivelRepo = despesaCartaoCombustivelRepo;
            _ticketLogTransacaoRepo = ticketLogTransacaoRepo;
            _ticketRepo = ticketRepo;
            _laudoRepo = laudoRepo;
            _instalacaoRepo = instalacaoRepo;
            _instalacaoSrv = instalacaoSrv;
            _pontoUsuarioRepo = pontoUsuarioRepo;
            _instalacaoPleitoRepo = instalacaoPleitoRepo;
            _instalacaoPleitoInstalRepo = instalacaoPleitoInstalRepo;
            _osBancadaPecasOrcamentoRepo = osBancadaPecasOrcamentoRepo;
            _osBancadaPecasRepo = osBancadaPecasRepo;
            _orcamentoPecasEspecRepo = orcamentoPecasEspecRepo;
            _usuarioService = usuarioService;
            _contextAcecssor = httpContextAccessor;
            _instalPagtoIntalRepo = instalPagtoIntalRepo;
            _ecSvc = equipamentoContratoSvc;
            FilePath = GenerateFilePath(".xlsx");
        }

        public dynamic Exportar(Exportacao exportacao)
        {
            switch (exportacao.FormatoArquivo)
            {
                case ExportacaoFormatoEnum.EXCEL:
                    return ExportExcel(exportacao.EntityParameters, exportacao.TipoArquivo);
                case ExportacaoFormatoEnum.PDF:
                    return ExportPDF(exportacao);
                case ExportacaoFormatoEnum.TXT:
                    return ExportTXT(exportacao.EntityParameters, exportacao.TipoArquivo);
                case ExportacaoFormatoEnum.ZIP:
                    return ExportZIP(exportacao);                    
                default:
                    return null;
            }
        }

        private IActionResult ExportPDF(Exportacao exportacao)
        {
            switch (exportacao.TipoArquivo)
            {
                case ExportacaoTipoEnum.ORCAMENTO:
                    return GerarPdfOrcamento(exportacao);
                case ExportacaoTipoEnum.ORDEM_SERVICO:
                    return GerarPdfOrdemServico(exportacao);
                case ExportacaoTipoEnum.DESPESA_PERIODO_TECNICO:
                    return GerarPdfDespesaPeriodoTecnico(exportacao);
                case ExportacaoTipoEnum.LAUDO:
                    return GerarPdfLaudo(exportacao);
                case ExportacaoTipoEnum.INSTALACAO_PLEITO:
                    return GerarPdfInstalacaoPleito(exportacao);
                case ExportacaoTipoEnum.ORC_BANCADA:
                    return GerarPdfOrcBancada(exportacao);
                case ExportacaoTipoEnum.NF_BANCADA:
                    return GerarPdfNFBancada(exportacao);
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
                    GerarPlanilhaOrdemServico(((JObject)parameters).ToObject<OrdemServicoParameters>());
                    break;

                case ExportacaoTipoEnum.EQUIPAMENTO_CONTRATO:
                    GerarPlanilhaEquipamentoContrato(((JObject)parameters).ToObject<EquipamentoContratoParameters>());
                    break;

                case ExportacaoTipoEnum.ACAO:
                    GerarPlanilhaAcao(((JObject)parameters).ToObject<AcaoParameters>());
                    break;

                case ExportacaoTipoEnum.ACAO_COMPONENTE:
                    GerarPlanilhaAcaoComponente(((JObject)parameters).ToObject<AcaoComponenteParameters>());
                    break;

                case ExportacaoTipoEnum.AUTORIZADA:
                    GerarPlanilhaAutorizada(((JObject)parameters).ToObject<AutorizadaParameters>());
                    break;

                case ExportacaoTipoEnum.CIDADE:
                    GerarPlanilhaCidade(((JObject)parameters).ToObject<CidadeParameters>());
                    break;

                case ExportacaoTipoEnum.TECNICO:
                    GerarPlanilhaTecnico(((JObject)parameters).ToObject<TecnicoParameters>());
                    break;

                case ExportacaoTipoEnum.CLIENTE:
                    GerarPlanilhaCliente(((JObject)parameters).ToObject<ClienteParameters>());
                    break;

                case ExportacaoTipoEnum.CLIENTEPECA:
                    GerarPlanilhaClientePeca(((JObject)parameters).ToObject<ClientePecaParameters>());
                    break;

                case ExportacaoTipoEnum.CLIENTEPECAGENERICA:
                    GerarPlanilhaClientePecaGenerica(((JObject)parameters).ToObject<ClientePecaGenericaParameters>());
                    break;

                case ExportacaoTipoEnum.CLIENTEBANCADA:
                    GerarPlanilhaClienteBancada(((JObject)parameters).ToObject<ClienteBancadaParameters>());
                    break;

                case ExportacaoTipoEnum.CONTRATO:
                    GerarPlanilhaContrato(((JObject)parameters).ToObject<ContratoParameters>());
                    break;

                case ExportacaoTipoEnum.DEFEITO:
                    GerarPlanilhaDefeito(((JObject)parameters).ToObject<DefeitoParameters>());
                    break;

                case ExportacaoTipoEnum.EQUIPAMENTO:
                    GerarPlanilhaEquipamento(((JObject)parameters).ToObject<EquipamentoParameters>());
                    break;

                case ExportacaoTipoEnum.GRUPOEQUIPAMENTO:
                    GerarPlanilhaGrupoEquipamento(((JObject)parameters).ToObject<GrupoEquipamentoParameters>());
                    break;

                case ExportacaoTipoEnum.TIPOEQUIPAMENTO:
                    GerarPlanilhaTipoEquipamento(((JObject)parameters).ToObject<TipoEquipamentoParameters>());
                    break;

                case ExportacaoTipoEnum.DEFEITOCOMPONENTE:
                    GerarPlanilhaDefeitoComponente(((JObject)parameters).ToObject<DefeitoComponenteParameters>());
                    break;

                case ExportacaoTipoEnum.EQUIPAMENTOMODULO:
                    GerarPlanilhaEquipamentoModulo(((JObject)parameters).ToObject<EquipamentoModuloParameters>());
                    break;

                case ExportacaoTipoEnum.FERRAMENTATECNICO:
                    GerarPlanilhaFerramentaTecnico(((JObject)parameters).ToObject<FerramentaTecnicoParameters>());
                    break;

                case ExportacaoTipoEnum.FILIAL:
                    GerarPlanilhaFilial(((JObject)parameters).ToObject<FilialParameters>());
                    break;

                case ExportacaoTipoEnum.FORMAPAGAMENTO:
                    GerarPlanilhaFormaPagamento(((JObject)parameters).ToObject<FormaPagamentoParameters>());
                    break;

                case ExportacaoTipoEnum.LIDERTECNICO:
                    GerarPlanilhaLiderTecnico(((JObject)parameters).ToObject<LiderTecnicoParameters>());
                    break;

                case ExportacaoTipoEnum.LOCALATENDIMENTO:
                    GerarPlanilhaLocalAtendimento(((JObject)parameters).ToObject<LocalAtendimentoParameters>());
                    break;

                case ExportacaoTipoEnum.PECA:
                    GerarPlanilhaPeca(((JObject)parameters).ToObject<PecaParameters>());
                    break;

                case ExportacaoTipoEnum.USUARIO:
                    GerarPlanilhaUsuario(((JObject)parameters).ToObject<UsuarioParameters>());
                    break;

                case ExportacaoTipoEnum.REGIAO:
                    GerarPlanilhaRegiao(((JObject)parameters).ToObject<RegiaoParameters>());
                    break;

                case ExportacaoTipoEnum.REGIAOAUTORIZADA:
                    GerarPlanilhaRegiaoAutorizada(((JObject)parameters).ToObject<RegiaoAutorizadaParameters>());
                    break;

                case ExportacaoTipoEnum.AUDITORIA:
                    GerarPlanilhaAuditoria(((JObject)parameters).ToObject<AuditoriaParameters>());
                    break;

                case ExportacaoTipoEnum.VALOR_COMBUSTIVEL:
                    GerarPlanilhaDespesaConfiguracaoCombustivel(((JObject)parameters).ToObject<DespesaConfiguracaoCombustivelParameters>());
                    break;

                case ExportacaoTipoEnum.ORCAMENTO:
                    GerarPlanilhaOrcamento(((JObject)parameters).ToObject<OrcamentoParameters>());
                    break;

                case ExportacaoTipoEnum.DESPESA_PERIODO_TECNICO:
                    GerarPlanilhaDespesaPeriodoTecnico(((JObject)parameters).ToObject<DespesaPeriodoTecnicoParameters>());
                    break;

                case ExportacaoTipoEnum.OR:
                    GerarPlanilhaOR(((JObject)parameters).ToObject<ORParameters>());
                    break;

                case ExportacaoTipoEnum.OR_ITEM:
                    GerarPlanilhaORItem(((JObject)parameters).ToObject<ORItemParameters>());
                    break;

                case ExportacaoTipoEnum.PEDIDOS_CREDITO:
                    GerarPlanilhaTicketLogPedidoCredito(((JObject)parameters).ToObject<TicketLogPedidoCreditoParameters>());
                    break;

                case ExportacaoTipoEnum.ORDEM_REPARO:
                    GerarPlanilhaOrdemReparo(((JObject)parameters).ToObject<ORParameters>());
                    break;

                case ExportacaoTipoEnum.OR_CHECKLIST:
                    GerarPlanilhaORCheckList(((JObject)parameters).ToObject<ORCheckListParameters>());
                    break;

                case ExportacaoTipoEnum.DESPESA_CARTAO_COMBUSTIVEL:
                    GerarPlanilhaDespesaCartaoCombustivel(((JObject)parameters).ToObject<DespesaCartaoCombustivelParameters>());
                    break;

                case ExportacaoTipoEnum.TICKET_LOG_TRANSACAO:
                    GerarPlanilhaTicketLogTransacao(((JObject)parameters).ToObject<TicketLogTransacaoParameters>());
                    break;

                case ExportacaoTipoEnum.TICKET:
                    GerarPlanilhaTicket(((JObject)parameters).ToObject<TicketParameters>());
                    break;
                    
                case ExportacaoTipoEnum.INSTALACAO:
                    GerarPlanilhaInstalacao(((JObject)parameters).ToObject<InstalacaoParameters>());
                    break;

                case ExportacaoTipoEnum.INSTALACAO_PLEITO:
                    GerarPlanilhaInstalacaoPleito(((JObject)parameters).ToObject<InstalacaoPleitoParameters>());
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

        private string GenerateFilePath(string file)
        {
            var fullPath = Path.Combine(Path.GetTempPath(), file);

            return fullPath;
        }

        private dynamic ExportTXT(dynamic parameters, ExportacaoTipoEnum tipo)
        {
            switch (tipo)
            {
                case ExportacaoTipoEnum.PONTO_USUARIO:
                    return GerarTXTPontoUsuario(((JObject)parameters).ToObject<UsuarioParameters>());
                default:
                    return null;
            }
        }
        
        private dynamic ExportZIP(Exportacao exportacao)
        {
            switch (exportacao.TipoArquivo)
            {
                case ExportacaoTipoEnum.ORDEM_SERVICO:
                    return GerarZipOrdemServico(exportacao);
                case ExportacaoTipoEnum.LAUDO:
                    return GerarZipLaudo(exportacao);
                case ExportacaoTipoEnum.INSTALACAO:
                    return GerarZipTermosInstalacao(exportacao);
                default:
                    return null;
            }
        }        
    }
}

