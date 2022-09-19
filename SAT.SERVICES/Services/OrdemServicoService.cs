using System;
using Newtonsoft.Json;
using NLog;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OrdemServicoService : IOrdemServicoService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IOrdemServicoRepository _ordemServicoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;
        private readonly IOrdemServicoAlertaService _ordemServicoAlertaService;
        private readonly IRelatorioAtendimentoRepository _ratRepo;
        private readonly IRelatorioAtendimentoDetalheRepository _ratDetalheRepo;
        private readonly IRelatorioAtendimentoDetalhePecaRepository _ratDetalhePecaRepo;

        public OrdemServicoService(
            IOrdemServicoRepository ordemServicoRepo, 
            ISequenciaRepository sequenciaRepo, 
            IOrdemServicoAlertaService ordemServicoAlertaService,
            IRelatorioAtendimentoRepository ratRepo,
            IRelatorioAtendimentoDetalheRepository ratDetalheRepo,
            IRelatorioAtendimentoDetalhePecaRepository ratDetalhePecaRepo
        ) {
            _ordemServicoRepo = ordemServicoRepo;
            _sequenciaRepo = sequenciaRepo;
            _ordemServicoAlertaService = ordemServicoAlertaService;
            _ratRepo = ratRepo;
            _ratDetalheRepo = ratDetalheRepo;
            _ratDetalhePecaRepo = ratDetalhePecaRepo;
        }

        public OrdemServico Atualizar(OrdemServico ordemServico)
        {
            try
            {
                _ordemServicoRepo.Atualizar(ordemServico);

                _logger.Info("Chamado Atualizado pelo SAT 2.0: " + JsonConvert.SerializeObject(ordemServico));            

                return ordemServico;                
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Erro ao atualizar chamado no SAT 2.0 {ex.Message}");
            }
        }

        public OrdemServico Criar(OrdemServico ordemServico)
        {
            ordemServico.CodOS = _sequenciaRepo.ObterContador("OS");

            _ordemServicoRepo.Criar(ordemServico);

            return ordemServico;
        }

        public OrdemServico Clonar(OrdemServico ordemServico)
        {
            try
            {
                var os = _ordemServicoRepo.ObterPorCodigo(ordemServico.CodOS);
                var codOS = _sequenciaRepo.ObterContador("OS");

                var novaOS = new OrdemServico {
                    CodOS = codOS,
                    CodCliente = os.CodCliente,
                    CodPosto = os.CodPosto,
                    CodEquipContrato = os.CodEquipContrato,
                    CodEquip = os.CodEquip,
                    CodTipoIntervencao = (int)TipoIntervencaoEnum.ORC_APROVADO,
                    CodFilial = os.CodFilial,
                    CodAutorizada = os.CodAutorizada,
                    CodRegiao = os.CodRegiao,
                    CodStatusServico = (int)StatusServicoEnum.ORCAMENTO,
                    DataHoraSolicitacao = DateTime.Now,
                    IndMarcaEspecial = 0,
                    DefeitoRelatado = os.DefeitoRelatado,
                    IndRevisaoReincidencia = os.IndRevisaoReincidencia,
                    IndCienciaAtendente = os.IndCienciaAtendente,
                    IndCienciaTerceirizada = os.IndCienciaTerceirizada,
                    DataHoraCad = DateTime.Now,
                    DataHoraAberturaOS = DateTime.Now,
                    IndCancelado = os.IndCancelado,
                    IndAtrasoSLA = os.IndAtrasoSLA,
                    IndEndossado = os.IndEndossado,
                    IndEnderecoRevisado = os.IndEnderecoRevisado,
                    IndIntegracao = os.IndIntegracao,
                    IndLiberacaoFechaduraCofre = os.IndLiberacaoFechaduraCofre,
                    NumReincidencia = os.NumReincidencia,
                    IndStatusEnvioReincidencia = os.IndStatusEnvioReincidencia
                };

                _ordemServicoRepo.Criar(novaOS);

                foreach (var r in os?.RelatoriosAtendimento)
                {
                    var codRAT = _sequenciaRepo.ObterContador("RAT");

                    var rat = new RelatorioAtendimento {
                        CodRAT = codRAT,
                        CodOS = novaOS.CodOS,
                        CodTecnico = Constants.TECNICO_SISTEMA,
                        CodStatusServico = r.CodStatusServico,
                        NumRAT = r.NumRAT,
                        NomeRespCliente = r.NomeRespCliente,
                        NomeAcompanhante = r.NomeAcompanhante,
                        DataHoraChegada = r.DataHoraChegada,
                        DataHoraInicio = r.DataHoraInicio,
                        DataHoraSolucao = r.DataHoraSolucao,
                        RelatoSolucao = r.RelatoSolucao,
                        ObsRAT = r.ObsRAT,
                        CodUsuarioCad = Constants.SISTEMA_NOME,
                        DataHoraCad = DateTime.Now,
                        HorarioInicioIntervalo = r.HorarioInicioIntervalo,
                        HorarioTerminoIntervalo = r.HorarioTerminoIntervalo
                    };

                    _ratRepo.Criar(rat);

                    foreach (var d in r?.RelatorioAtendimentoDetalhes)
                    {
                        var codRATDetalhe = _sequenciaRepo.ObterContador("RATDetalhes");

                        var detalhe = new RelatorioAtendimentoDetalhe {
                            CodRATDetalhe = codRATDetalhe,
                            CodRAT = rat.CodRAT,
                            CodServico = d.CodServico,
                            CodTipoCausa = d.CodTipoCausa,
                            CodGrupoCausa = d.CodGrupoCausa,
                            CodDefeito = d.CodDefeito,
                            CodAcao = d.CodAcao,
                            CodCausa = d.CodCausa,
                            CodUsuarioCad = d.CodUsuarioCad,
                            DataHoraCad = d.DataHoraCad,
                            CodOrigemCausa = d.CodOrigemCausa,
                            CodOS = d.CodOS
                        };

                        _ratDetalheRepo.Criar(detalhe);

                        foreach (var p in d?.RelatorioAtendimentoDetalhePecas)
                        {
                            var codRATDetalhePeca = _sequenciaRepo.ObterContador("RATDetalhesPecas");

                            var peca = new RelatorioAtendimentoDetalhePeca {
                                CodRATDetalhePeca = codRATDetalhePeca,
                                CodMagnusInconsistente = p.CodMagnusInconsistente,
                                CodUsuarioManutencao = p.CodUsuarioManutencao,
                                IndPassivelConserto = p.IndPassivelConserto,
                                CodUsuarioManut = p.CodUsuarioManut,
                                DataManutencao = p.DataManutencao,
                                CodRATDetalhe = codRATDetalhe,
                                CodUsuarioCad = p.CodUsuarioCad,
                                DataHoraCad = p.DataHoraCad,
                                DescStatus = p.DescStatus,
                                CodPeca = p.CodPeca,
                                QtdePecas = p.QtdePecas,
                                ValPecas = p.ValPecas,
                                DatIncPP = p.DatIncPP,
                                QtdeLib = p.QtdeLib,
                                IndOK = p.IndOK,
                                AP = p.AP
                            };

                            _ratDetalhePecaRepo.Criar(peca);
                        }
                    }
                }

                return novaOS;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao clonar a OS {ordemServico.CodOS}: " + ex.Message);
            }
        }

        public void Deletar(int codOS)
        {
            _ordemServicoRepo.Deletar(codOS);
        }

        public OrdemServico ObterPorCodigo(int codigo)
        {
            OrdemServico os = _ordemServicoRepo.ObterPorCodigo(codigo);

            if (os != null)
            {
                os.Alertas = _ordemServicoAlertaService.ObterAlertas(os);
                os.IndNumRATObrigatorio = VerificarNumeroRATObrigatorio(os);
            }

            return os;
        }

        public ListViewModel ObterPorParametros(OrdemServicoParameters parameters)
        {
            var ordensServico = _ordemServicoRepo.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = ordensServico,
                TotalCount = ordensServico.TotalCount,
                CurrentPage = ordensServico.CurrentPage,
                PageSize = ordensServico.PageSize,
                TotalPages = ordensServico.TotalPages,
                HasNext = ordensServico.HasNext,
                HasPrevious = ordensServico.HasPrevious
            };
        }

        private bool VerificarNumeroRATObrigatorio(OrdemServico os)
        {
            if (os.CodTipoIntervencao == (int)TipoIntervencaoEnum.INSTALACAO)
            {
                return true;
            }

            if ((
                    os.CodTipoIntervencao == (int)TipoIntervencaoEnum.CORRETIVA
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.PREVENTIVA
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_APROVADO
                )
                &&
                (
                    os.CodCliente == (int)ClienteEnum.ZAFFARI
                    || os.CodCliente == (int)ClienteEnum.BANRISUL
                    || os.CodCliente == (int)ClienteEnum.CEF
                    || os.CodCliente == (int)ClienteEnum.SICREDI
                    || os.CodCliente == (int)ClienteEnum.CORREIOS
                    || os.CodCliente == (int)ClienteEnum.BB
                    || os.CodCliente == (int)ClienteEnum.BANPARA
                    || os.CodCliente == (int)ClienteEnum.BRB
                    || os.CodCliente == (int)ClienteEnum.BRB_OUTSOURCING
                    || os.CodCliente == (int)ClienteEnum.BANCO_DA_AMAZONIA
                    || os.CodCliente == (int)ClienteEnum.BNB
                    || os.CodCliente == (int)ClienteEnum.BANESTES
                ))
            {
                return true;
            }

            if (os.CodCliente == (int)ClienteEnum.BB
                &&
                (os.CodTipoIntervencao == (int)TipoIntervencaoEnum.CORRETIVA
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.PREVENTIVA
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORCAMENTO
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_APROVADO
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_REPROVADO
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_PEND_APROVACAO_CLIENTE
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_PEND_FILIAL_DETALHAR_MOTIVO
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.INSPECAO_TECNICA)
                &&
                os.CodContrato != (int)ContratoEnum.BB_TECNO_0125_2017)
            {
                return true;
            }

            if (os.CodCliente == (int)ClienteEnum.BB
                &&
                (os.CodTipoIntervencao == (int)TipoIntervencaoEnum.CORRETIVA
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.PREVENTIVA
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORCAMENTO
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_APROVADO
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_REPROVADO
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_PEND_APROVACAO_CLIENTE
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_PEND_FILIAL_DETALHAR_MOTIVO
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.INSPECAO_TECNICA
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.MANUTENCAO_GERENCIAL
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.COFRE)
                &&
                os.CodContrato == (int)ContratoEnum.BB_TECNO_0125_2017)
            {
                return true;
            }

            return false;
        }        
    }
}