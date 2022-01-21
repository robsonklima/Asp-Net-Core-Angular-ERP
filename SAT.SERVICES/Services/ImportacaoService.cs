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
    public class ImportacaoService : IImportacaoService
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

        private List<string> AberturaChamadosEmMassa(List<ImportacaoAberturaOrdemServico> importacaoOs)
        {
            var osMensagem = new List<string>();

            importacaoOs.Where(o => o.CodEquipContrato is not null)
                .ToList()
                .ForEach(i =>
                {

                    var equipamento = _equipamentoContratoRepo.ObterPorCodigo(i.CodEquipContrato.Value);

                    if (equipamento is null) osMensagem.Add($"Equipamento não encontrado - ID: {i.CodEquipContrato}");

                    var local = equipamento is not null ? _localAtendimentoRepo.ObterPorCodigo(equipamento.CodPosto) : null;

                    if (local is not null && equipamento is not null)
                    {
                        try
                        {
                            int codOs = _sequenciaRepo.ObterContador("OS");

                            var os = new OrdemServico
                            {
                                CodOS = codOs,
                                CodCliente = local.CodCliente,
                                CodPosto = local.CodPosto.Value,
                                CodTipoIntervencao = int.Parse(i.TipoIntervencao),
                                CodFilial = equipamento.CodFilial,
                                CodRegiao = equipamento.CodRegiao,
                                CodAutorizada = equipamento.CodAutorizada,
                                CodEquipContrato = equipamento.CodEquipContrato,
                                CodEquip = equipamento.CodEquip,
                                CodUsuarioCad = _contextAcecssor.HttpContext.User.Identity.Name,
                                DefeitoRelatado = i.DefeitoRelatado,
                                NumOSCliente = i.NumOSCliente,
                                NumOSQuarteirizada = i.NumOSQuarteirizada,
                                DataHoraCad = DateTime.Now,
                                DataHoraSolicitacao = DateTime.Now,
                                DataHoraAberturaOS = DateTime.Now,
                                IndStatusEnvioReincidencia = -1,
                                IndRevisaoReincidencia = 1,
                                CodStatusServico = 1
                            };

                            _ordemServicoRepo.Criar(os);

                            osMensagem.Add($"Chamado criado - {codOs} - Série: {equipamento.NumSerie}");
                        }
                        catch (Exception ex)
                        {
                            osMensagem.Add($"Erro ao criar (exceção gerada) - Série: {equipamento.NumSerie} ID: {equipamento.CodEquipContrato}");
                        }
                    }
                });

            var usuario = _usuarioService.ObterPorCodigo(_contextAcecssor.HttpContext.User.Identity.Name);

            _emailService.Enviar(new Email
            {
                NomeRemetente = "SAT",
                EmailRemetente = "equipe.sat@perto.com.br",
                NomeDestinatario = usuario.NomeUsuario,
                EmailDestinatario = usuario.Email,
                NomeCC = "SAT",
                EmailCC = "equipe.sat@perto.com.br",
                Assunto = "Abertura de chamados em massa",
                Corpo = GerarHTML<string>(osMensagem)
            });

            return osMensagem;
        }

        private string GerarHTML<T>(List<T> list, params string[] columns)
        {
            var sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append($"{item}<br>");
            }

            sb.Append(Constants.ASSINATURA_EMAIL);
            return sb.ToString();
        }

        private List<string> AtualizacaoInstalacao(List<ImportacaoInstalacao> importacaoInstalacao)
        {

            return new List<string>();
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