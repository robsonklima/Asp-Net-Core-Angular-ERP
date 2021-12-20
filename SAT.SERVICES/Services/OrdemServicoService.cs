using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Linq;
using System;
using SAT.MODELS.Entities.Constants;

namespace SAT.SERVICES.Services
{
    public class OrdemServicoService : IOrdemServicoService
    {
        private readonly IOrdemServicoRepository _ordemServicoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;
        private readonly IOrdemServicoAlertaService _ordemServicoAlertaService;
        public OrdemServicoService(IOrdemServicoRepository ordemServicoRepo, ISequenciaRepository sequenciaRepo, IOrdemServicoAlertaService ordemServicoAlertaService)
        {
            _ordemServicoRepo = ordemServicoRepo;
            _sequenciaRepo = sequenciaRepo;
            _ordemServicoAlertaService = ordemServicoAlertaService;
        }

        public OrdemServico Atualizar(OrdemServico ordemServico)
        {
            _ordemServicoRepo.Atualizar(ordemServico);

            return ordemServico;
        }

        public OrdemServico Criar(OrdemServico ordemServico)
        {
            ordemServico.CodOS = _sequenciaRepo.ObterContador("OS");

            _ordemServicoRepo.Criar(ordemServico);

            return ordemServico;
        }

        public void Deletar(int codOS)
        {
            _ordemServicoRepo.Deletar(codOS);
        }

        public OrdemServico ObterPorCodigo(int codigo)
        {
            OrdemServico os = _ordemServicoRepo.ObterPorCodigo(codigo);

            os.Alertas = _ordemServicoAlertaService.ObterAlertas(os);
            os.IndNumRATObrigatorio = VerificarNumeroRATObrigatorio(os);

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

            if (
                (
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
                )
            )
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
        public IActionResult ExportToExcel(OrdemServicoParameters parameters)
        {
            var os = _ordemServicoRepo
                        .ObterPorParametros(parameters);

            var listaExcel = new List<List<object>>();

            listaExcel.Add(os.Select(os =>
                        new OrdemServicoExcelViewModel()
                        {
                            Chamado = os.CodOS,
                            DataAbertura = os.DataHoraAberturaOS?.ToString() ?? Constants.SEM_NADA,
                            DataSolicitacao = os.DataHoraSolicitacao?.ToString() ?? Constants.SEM_NADA,
                            LimiteAtendimento = os.PrazosAtendimento?.FirstOrDefault()?.DataHoraLimiteAtendimento?.ToString() ?? Constants.SEM_NADA,
                            Status = os.StatusServico?.NomeStatusServico ?? Constants.SEM_NADA,
                            Intervencao = os.TipoIntervencao?.CodETipoIntervencao ?? Constants.SEM_NADA,
                            Defeito = os.DefeitoRelatado ?? Constants.SEM_NADA,
                            PA = os.RegiaoAutorizada?.PA ?? 0,
                            Local = os.LocalAtendimento?.NomeLocal ?? Constants.SEM_NADA,
                            Regiao = os.EquipamentoContrato?.Regiao?.NomeRegiao ?? Constants.SEM_NADA,
                            Autorizada = os.EquipamentoContrato?.Autorizada?.NomeFantasia ?? Constants.SEM_NADA,
                            NumBanco = os.Cliente?.NumBanco ?? Constants.SEM_NADA,
                            SLA = os.EquipamentoContrato?.AcordoNivelServico?.NomeSLA ?? Constants.SEM_NADA,
                            Equipamento = os.Equipamento?.NomeEquip ?? Constants.SEM_NADA,
                            Serie = os.EquipamentoContrato?.NumSerie ?? Constants.SEM_NADA,
                            Reincidencia = os.NumReincidencia ?? 0
                        }).ToList<object>());

            listaExcel.Add(os.SelectMany(os => os.RelatoriosAtendimento.Select(r =>
                        {
                            return new RatExcelViewModel
                            {
                                Chamado = r.CodOS,
                                Rat = r.NumRAT ?? Constants.SEM_NADA,
                                RelatoSolucao = r.RelatoSolucao ?? Constants.SEM_NADA,
                                Tecnico = r.Tecnico?.Nome ?? Constants.SEM_NADA,
                                Status = r.StatusServico?.NomeStatusServico ?? Constants.SEM_NADA,
                                Data = r.DataHoraSolucao.Date.ToString() ?? Constants.SEM_NADA,
                                Hora = r.DataHoraSolucao.ToString("HH:mm"),
                                TipoServico = r.TipoServico?.NomeServico ?? Constants.SEM_NADA,
                                Observacao = r.ObsRAT ?? Constants.SEM_NADA
                            };
                        })).ToList<object>());

            return new ExcelService<OrdemServico>().ExportExcel(listaExcel);
        }
    }
}