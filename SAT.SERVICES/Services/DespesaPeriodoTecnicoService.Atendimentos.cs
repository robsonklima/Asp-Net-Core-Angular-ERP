using System;
using System.Collections.Generic;
using SAT.MODELS.Entities.Params;
using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class DespesaPeriodoTecnicoService : IDespesaPeriodoTecnicoService
    {
        private PagedList<DespesaPeriodo> ObterPeriodos(DespesaPeriodoTecnicoParameters parameters)
        {
            var periodos = _despesaPeriodoRepo.ObterQuery(
                new DespesaPeriodoParameters
                {
                    IndAtivo = parameters.IndAtivoPeriodo,
                    InicioPeriodo = parameters.InicioPeriodo,
                    FimPeriodo = parameters.FimPeriodo,
                    SortActive = parameters.SortActive,
                    SortDirection = parameters.SortDirection
                });

            if (!string.IsNullOrEmpty(parameters.CodDespesaPeriodoTecnicoStatus) && !parameters.CodDespesaPeriodoTecnicoStatus.Contains("0"))
            {
                var periodosTecnico = _despesaPeriodoTecnicoRepo.ObterPorParametros(
                        new DespesaPeriodoTecnicoParameters
                        {
                            CodTecnico = parameters.CodTecnico.ToString(),
                            CodDespesaPeriodoTecnicoStatus = parameters.CodDespesaPeriodoTecnicoStatus
                        }).Select(p => p.CodDespesaPeriodo);

                periodos =
                    periodos.Where(p => periodosTecnico.Any(s => p.CodDespesaPeriodo == s));
            }

            return PagedList<DespesaPeriodo>
                    .ToPagedList(periodos, parameters.PageNumber, parameters.PageSize);
        }

        private DespesaPeriodoTecnico ObterDespesaPeriodoTecnico(string codTecnico, int codPeriodo) =>
            _despesaPeriodoTecnicoRepo.ObterPorParametros(
                new DespesaPeriodoTecnicoParameters
                {
                    CodTecnico = codTecnico.ToString(),
                    CodDespesaPeriodo = codPeriodo
                }).FirstOrDefault();

        private string ObterDespesaPeriodoTecnicoStatus(string codTecnico, int codPeriodo)
        {
            var despesa = ObterDespesaPeriodoTecnico(codTecnico, codPeriodo);

            if (despesa != null)
                return despesa.CodDespesaPeriodoTecnicoStatus.ToString();

            return string.Empty;
        }


        private List<DespesaAdiantamentoPeriodo> ObterDespesasPeriodoAdiantamentos(string codTecnico, int? codPeriodo = null) =>
            _despesaAdiantamentoPeriodoService.ObterDespesasPeriodoAdiantamentos(
                new DespesaAdiantamentoPeriodoParameters
                {
                    CodTecnico = Convert.ToInt32(codTecnico),
                    CodDespesaPeriodo = codPeriodo,
                    IndAdiantamentoAtivo = 1
                });

        private decimal TotalDespesa(string codTecnico, int codPeriodo) =>
            _despesaRepository.ObterPorParametros(
                new DespesaParameters
                {
                    CodTecnico = codTecnico,
                    CodDespesaPeriodo = codPeriodo
                }).Sum(d =>
                    d.DespesaItens?.Where(di =>
                        di.IndAtivo == 1 &&
                        di.CodDespesaTipo != (int)DespesaTipoEnum.KM)
                    .Sum(di => di.DespesaValor)) ?? 0;

        private decimal TotalAdiantamentoUtilizado(string codTecnico, int codPeriodo) =>
            this.ObterDespesasPeriodoAdiantamentos(codTecnico, codPeriodo)
                .Sum(a => a.ValorAdiantamentoUtilizado);

        private decimal ObterTotalAdiantamento(string codTecnico, int codPeriodo) =>
            this.ObterDespesasPeriodoAdiantamentos(codTecnico, codPeriodo)
            .Select(i => i.DespesaAdiantamento)
            .Sum(i => i.ValorAdiantamento);

        private decimal ObterTotalAdiantamentoProvisorio(string codTecnico, int codPeriodo) =>
            this.ObterDespesasPeriodoAdiantamentos(codTecnico, codPeriodo)
            .Select(i => i.DespesaAdiantamento)
            .Where(i => i.CodDespesaAdiantamento == (int)DespesaAdiantamentoEnum.PROVISORIO)
            .Sum(i => i.ValorAdiantamento);

        private decimal TotalGastosExcedentes(decimal totalDespesa, decimal totalAdiantamento)
        {
            var saldo = totalAdiantamento - totalDespesa;

            if (totalDespesa < totalAdiantamento)
                return 0;
            else if (saldo < 0)
                return saldo * -1;
            else return saldo;
        }

        private decimal TotalRestituicao(decimal totalDespesa, decimal totalAdiantamentoProvisorio)
        {
            var saldo = totalAdiantamentoProvisorio - totalDespesa;

            if (totalDespesa > totalAdiantamentoProvisorio)
                return 0;
            else if (saldo < 0)
                return saldo * -1;
            else return saldo;
        }

        private List<DespesaPeriodoTecnicoAtendimentoItem> CalculaDespesasPorPeriodo(List<DespesaPeriodo> periodos, string codTecnico) =>
            periodos.Select(despesa =>
            {
                var despesaPeriodoTecnico =
                    this.ObterDespesaPeriodoTecnico(codTecnico, despesa.CodDespesaPeriodo);

                var totalDespesa = this.TotalDespesa(codTecnico, despesa.CodDespesaPeriodo);
                var totalAdiantamento = this.ObterTotalAdiantamento(codTecnico, despesa.CodDespesaPeriodo);
                var totalAdiantamentoProvisorio = this.ObterTotalAdiantamentoProvisorio(codTecnico, despesa.CodDespesaPeriodo);

                return new DespesaPeriodoTecnicoAtendimentoItem
                {
                    CodDespesaPeriodo = despesa.CodDespesaPeriodo,
                    CodDespesaPeriodoTecnico = despesaPeriodoTecnico?.CodDespesaPeriodoTecnico,
                    CodTecnico = codTecnico,
                    DataInicio = despesa.DataInicio,
                    DataFim = despesa.DataFim,
                    TotalDespesa = totalDespesa,
                    TotalAdiantamento = this.TotalAdiantamentoUtilizado(codTecnico, despesa.CodDespesaPeriodo),
                    GastosExcedentes = this.TotalGastosExcedentes(totalDespesa, totalAdiantamento),
                    RestituirAEmpresa = this.TotalRestituicao(totalDespesa, totalAdiantamentoProvisorio),
                    Status = despesaPeriodoTecnico?.DespesaPeriodoTecnicoStatus,
                    IndAtivo = Convert.ToBoolean(despesa.IndAtivo)
                };
            }).ToList();

        public ListViewModel ObterAtendimentos(DespesaPeriodoTecnicoParameters parameters)
        {
            if (string.IsNullOrEmpty(parameters.CodTecnico)) return new ListViewModel();

            var despesasPeriodo = this.ObterPeriodos(parameters);
            var despesasPeriodoTecnico = this.CalculaDespesasPorPeriodo(despesasPeriodo, parameters.CodTecnico);

            var lista = new ListViewModel
            {
                Items = despesasPeriodoTecnico,
                TotalCount = despesasPeriodo.TotalCount,
                CurrentPage = despesasPeriodo.CurrentPage,
                PageSize = despesasPeriodo.PageSize,
                TotalPages = despesasPeriodo.TotalPages,
                HasNext = despesasPeriodo.HasNext,
                HasPrevious = despesasPeriodo.HasPrevious
            };

            return lista;
        }
    }
}