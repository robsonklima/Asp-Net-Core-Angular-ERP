using System.Collections.Generic;
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
        private readonly IEquipamentoRepository _equipamentoRepo;
        private readonly IClienteRepository _clienteRepo;
        private readonly IAutorizadaRepository _autorizadaRepo;
        private readonly IRegiaoRepository _regiaoRepo;
        private readonly ITipoEquipamentoRepository _tipoEquipRepo;
        private readonly IGrupoEquipamentoRepository _grupoEquipRepo;

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
            IUsuarioService usuarioService,
            IEquipamentoRepository equipamentoRepo,
            IClienteRepository clienteRepo,
            IAutorizadaRepository autorizadaRepo,
            IRegiaoRepository regiaoRepo,
            ITipoEquipamentoRepository tipoEquipRepo,
            IGrupoEquipamentoRepository grupoEquipRepo
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
            _equipamentoRepo = equipamentoRepo;
            _clienteRepo = clienteRepo;
            _autorizadaRepo = autorizadaRepo;
            _regiaoRepo = regiaoRepo;
            _tipoEquipRepo = tipoEquipRepo;
            _grupoEquipRepo = grupoEquipRepo;
        }

        public Importacao Importar(Importacao importacao)
        {
            switch (importacao.Id)
            {
                case (int)ImportacaoEnum.INSTALACAO:
                    return ImportacaoInstalacao(importacao);

                case (int)ImportacaoEnum.ORDEM_SERVICO:
                    return ImportacaoOrdemServico(importacao);

                default:
                    return null;
            }
        }
    }
}