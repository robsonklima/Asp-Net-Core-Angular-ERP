using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using SAT.MODELS.Entities.Constants;

namespace SAT.SERVICES.Services
{
    public partial class ImportacaoService : IImportacaoService
    {
        private readonly IOrdemServicoRepository _ordemServicoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;
        private readonly ILocalAtendimentoRepository _localAtendimentoRepo;
        private readonly IEquipamentoContratoRepository _equipamentoContratoRepo;
        private readonly IHttpContextAccessor _contextAcecssor;
        private readonly IEmailService _emailService;
        private readonly IUsuarioService _usuarioService;

        public ImportacaoService(
            IOrdemServicoRepository ordemServicoRepo,
            ISequenciaRepository sequenciaRepo,
            ILocalAtendimentoRepository localAtendimentoRepo,
            IEquipamentoContratoRepository equipamentoContratoRepo,
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService,
            IUsuarioService usuarioService
            )
        {
            _ordemServicoRepo = ordemServicoRepo;
            _sequenciaRepo = sequenciaRepo;
            _localAtendimentoRepo = localAtendimentoRepo;
            _equipamentoContratoRepo = equipamentoContratoRepo;
            _contextAcecssor = httpContextAccessor;
            _emailService = emailService;
            _usuarioService = usuarioService;
        }

        public List<string> Importacao(ImportacaoBase importacao)
        {
            switch (importacao.Id)
            {
                case (int)ImportacaoEnum.ATUALIZACAO_IMPLANTACAO:

                    return AtualizacaoInstalacao(JsonConvert.DeserializeObject<List<ImportacaoInstalacao>>(importacao.JsonImportacao));

                case (int)ImportacaoEnum.ABERTURA_CHAMADOS_EM_MASSA:

                    return AberturaChamadosEmMassa(JsonConvert.DeserializeObject<List<ImportacaoAberturaOrdemServico>>(importacao.JsonImportacao));

                default:

                    return null;
            }
        }
    }
}