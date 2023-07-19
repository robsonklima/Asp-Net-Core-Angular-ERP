using System.Linq;
using Microsoft.AspNetCore.Http;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class ImportacaoService : IImportacaoService
    {
        private readonly IOrdemServicoRepository _ordemServicoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;
        private readonly ILocalAtendimentoRepository _localAtendimentoRepo;
        private readonly IEquipamentoContratoRepository _equipamentoContratoRepo;
        private readonly IInstalacaoRepository _instalacaoRepo;
        private readonly IInstalacaoPagtoInstalRepository _instalacaoPagtoInstalRepo;
        private readonly IInstalacaoNFVendaRepository _instalacaoNFVendaRepo;
        private readonly IHttpContextAccessor _contextAcecssor;
        private readonly IEmailService _emailService;
        private readonly IContratoRepository _contratoRepo;
        private readonly IUsuarioService _usuarioService;
        private readonly IEquipamentoRepository _equipamentoRepo;
        private readonly IClienteRepository _clienteRepo;
        private readonly IAutorizadaRepository _autorizadaRepo;
        private readonly IRegiaoRepository _regiaoRepo;
        private readonly ITipoEquipamentoRepository _tipoEquipRepo;
        private readonly IGrupoEquipamentoRepository _grupoEquipRepo;
        private readonly IAcordoNivelServicoRepository _slaRepo;
        private readonly IInstalacaoStatusRepository _instalStatusRepo;
        private readonly IFilialRepository _filialRepo;
        private readonly IInstalacaoLoteRepository _instalLoteRepo;
        private readonly IInstalacaoTipoParcelaRepository _instalTipoParcelaRepo;
        private readonly IInstalacaoPagtoRepository _instalacaoPagtoRepo;       
        private readonly IInstalacaoService _instalacaoService;
        private readonly IInstalacaoPagtoInstalService _instalacaoPagtoInstalService;
        private readonly IEquipamentoContratoService _equipamentoContratoService;
        private readonly IORItemRepository _orItemRepo;        
        private readonly IORItemService _orItemService;             
        private readonly IUsuarioRepository _usuarioRepo;      
        private readonly IPecaRepository _pecaRepo;    
        private readonly IORStatusRepository _orStatusRepo;
        private readonly ICidadeRepository _cidadeRepo;

        public ImportacaoService(
            IOrdemServicoRepository ordemServicoRepo,
            ISequenciaRepository sequenciaRepo,
            ILocalAtendimentoRepository localAtendimentoRepo,
            IEquipamentoContratoRepository equipamentoContratoRepo,
            IInstalacaoRepository instalacaoRepo,
            IInstalacaoPagtoInstalRepository instalacaoPagtoInstalRepo,
            IInstalacaoNFVendaRepository instalacaoNFVendaRepo,
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService,
            IContratoRepository contratoRepo,
            IUsuarioService usuarioService,
            IEquipamentoRepository equipamentoRepo,
            IClienteRepository clienteRepo,
            IAutorizadaRepository autorizadaRepo,
            IRegiaoRepository regiaoRepo,
            ITipoEquipamentoRepository tipoEquipRepo,
            IGrupoEquipamentoRepository grupoEquipRepo,
            IAcordoNivelServicoRepository slaRepo,
            IInstalacaoStatusRepository instalStatusRepo,
            IFilialRepository filialRepo,
            IInstalacaoLoteRepository instalLoteRepo,
            IInstalacaoTipoParcelaRepository instalTipoParcelaRepo,
            IInstalacaoPagtoRepository instalacaoPagtoRepo,    
            IInstalacaoService instalacaoService,
            IInstalacaoPagtoInstalService instalacaoPagtoInstalService,
            IEquipamentoContratoService equipamentoContratoService,
            IORItemRepository orItemRepo,       
            IORItemService orItemService,             
            IUsuarioRepository usuarioRepo,
            IPecaRepository pecaRepo,
            IORStatusRepository orStatusRepo,
            IORItemInsumoRepository orItemInsumoRepo,
            ICidadeRepository cidadeRepo       
            )
        {
            _ordemServicoRepo = ordemServicoRepo;
            _sequenciaRepo = sequenciaRepo;
            _localAtendimentoRepo = localAtendimentoRepo;
            _equipamentoContratoRepo = equipamentoContratoRepo;
            _instalacaoRepo = instalacaoRepo;
            _instalacaoPagtoInstalRepo = instalacaoPagtoInstalRepo;
            _instalacaoNFVendaRepo = instalacaoNFVendaRepo;
            _contextAcecssor = httpContextAccessor;
            _emailService = emailService;
            _contratoRepo = contratoRepo;
            _usuarioService = usuarioService;
            _equipamentoRepo = equipamentoRepo;
            _clienteRepo = clienteRepo;
            _autorizadaRepo = autorizadaRepo;
            _regiaoRepo = regiaoRepo;
            _tipoEquipRepo = tipoEquipRepo;
            _grupoEquipRepo = grupoEquipRepo;
            _slaRepo = slaRepo;
            _instalStatusRepo = instalStatusRepo;
            _filialRepo = filialRepo;
            _instalLoteRepo = instalLoteRepo;
            _instalTipoParcelaRepo = instalTipoParcelaRepo;
            _instalacaoPagtoRepo = instalacaoPagtoRepo;         
            _instalacaoService = instalacaoService;
            _instalacaoPagtoInstalService = instalacaoPagtoInstalService;
            _equipamentoContratoService = equipamentoContratoService;
            _orItemRepo = orItemRepo;
            _orItemService = orItemService;   
            _usuarioRepo = usuarioRepo; 
            _pecaRepo = pecaRepo; 
            _orStatusRepo = orStatusRepo;    
            _cidadeRepo = cidadeRepo;      
        }

        private dynamic ConverterCamposEmComum(ImportacaoColuna coluna)
        {
            switch (coluna.Campo)
            {
                case "NomeSla":
                    return _slaRepo.ObterPorParametros(new AcordoNivelServicoParameters { NomeSLA = coluna.Valor })?.FirstOrDefault()?.CodSLA;
                case "NomeEquipamento":
                    return _equipamentoRepo.ObterPorParametros(new EquipamentoParameters { Filter = coluna.Valor })?.FirstOrDefault()?.CodEquip;
                case "NomeGrupoEquip":
                    return _grupoEquipRepo.ObterPorParametros(new GrupoEquipamentoParameters { Filter = coluna.Valor })?.FirstOrDefault()?.CodGrupoEquip;
                case "NomeTipoEquip":
                    return _tipoEquipRepo.ObterPorParametros(new TipoEquipamentoParameters { Filter = coluna.Valor })?.FirstOrDefault()?.CodTipoEquip;
                case "NomeContrato":
                    return _contratoRepo.ObterPorParametros(new ContratoParameters { Filter = coluna.Valor })?.FirstOrDefault()?.CodContrato;
                case "NroContrato":
                    return _contratoRepo.ObterPorParametros(new ContratoParameters { NroContrato = coluna.Valor })?.FirstOrDefault()?.CodContrato;
                case "NomeRegiao":
                    return _regiaoRepo.ObterPorParametros(new RegiaoParameters { NomeRegiao = coluna.Valor })?.FirstOrDefault()?.CodRegiao;
                case "NomeAutorizada":
                    return _autorizadaRepo.ObterPorParametros(new AutorizadaParameters { Filter = coluna.Valor })?.FirstOrDefault()?.CodAutorizada;
                case "NomeCliente":
                    return _clienteRepo.ObterPorParametros(new ClienteParameters { NomeFantasia = coluna.Valor })?.FirstOrDefault()?.CodCliente;
                case "NomeInstalStatus":
                    return _instalStatusRepo.ObterPorParametros(new InstalacaoStatusParameters { NomeInstalStatus = coluna.Valor })?.FirstOrDefault()?.CodInstalStatus;
                case "NomeFilial":
                    return _filialRepo.ObterPorParametros(new FilialParameters { Filter = coluna.Valor })?.FirstOrDefault()?.CodFilial;
                case "NomeLote":
                    return _instalLoteRepo.ObterPorParametros(new InstalacaoLoteParameters { Filter = coluna.Valor })?.FirstOrDefault()?.CodInstalLote;
                case "NomeUsuario":
                    return _usuarioRepo.ObterPorParametros(new UsuarioParameters { Filter = coluna.Valor })?.FirstOrDefault()?.CodUsuario;                    
                default:
                    return coluna;
            }
        }

        public Importacao Importar(Importacao importacao)
        {
            switch (importacao.Id)
            {
                case (int)ImportacaoEnum.INSTALACAO:
                    return ImportacaoInstalacao(importacao);
                case (int)ImportacaoEnum.ORDEM_SERVICO:
                    return ImportacaoOrdemServico(importacao);
                case (int)ImportacaoEnum.EQUIPAMENTO_CONTRATO:
                    return ImportacaoEquipamentoContrato(importacao);
                case (int)ImportacaoEnum.INSTALACAO_PAGTO_INSTAL:
                    return ImportacaoInstalacaoPagtoInstal(importacao);
                case (int)ImportacaoEnum.PROCESSO_REPARO:
                    return ImportacaoProcessoReparo(importacao);    
                case (int)ImportacaoEnum.ADENDO:
                    return ImportacaoAdendo(importacao);                
                default:
                    return null;
            }
        }
    }
}