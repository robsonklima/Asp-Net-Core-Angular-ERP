using Microsoft.AspNetCore.Http;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
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
        private readonly IInstalacaoNFVendaRepository _instalacaoNFVendaRepo;
        private readonly IHttpContextAccessor _contextAcecssor;
        private readonly IEmailService _emailService;
        private readonly IContratoRepository _contratoRepo;
        private readonly IUsuarioService _usuarioService;

        public ImportacaoService(
            IOrdemServicoRepository ordemServicoRepo,
            ISequenciaRepository sequenciaRepo,
            ILocalAtendimentoRepository localAtendimentoRepo,
            IEquipamentoContratoRepository equipamentoContratoRepo,
            IInstalacaoRepository instalacaoRepo,
            IInstalacaoNFVendaRepository instalacaoNFVendaRepo,
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService,
            IContratoRepository contratoRepo,
            IUsuarioService usuarioService
            )
        {
            _ordemServicoRepo = ordemServicoRepo;
            _sequenciaRepo = sequenciaRepo;
            _localAtendimentoRepo = localAtendimentoRepo;
            _equipamentoContratoRepo = equipamentoContratoRepo;
            _instalacaoRepo = instalacaoRepo;
            _instalacaoNFVendaRepo = instalacaoNFVendaRepo;
            _contextAcecssor = httpContextAccessor;
            _emailService = emailService;
            _contratoRepo = contratoRepo;
            _usuarioService = usuarioService;
        }

        public Importacao Importar(Importacao importacao)
        {

            switch (importacao.Id)
            {
                case (int)ImportacaoEnum.ATUALIZACAO_IMPLANTACAO:
                    return AtualizacaoInstalacao(importacao);

                case (int)ImportacaoEnum.ABERTURA_CHAMADOS_EM_MASSA:
                    return AberturaChamadosEmMassa(importacao);

                default:
                    return null;
            }
        }
    }
}