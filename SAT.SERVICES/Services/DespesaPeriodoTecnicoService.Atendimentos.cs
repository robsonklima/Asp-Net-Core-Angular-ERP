using System;
using System.Collections.Generic;
using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class DespesaPeriodoTecnicoService : IDespesaPeriodoTecnicoService
    {
        private PagedList<DespesaPeriodo> ObterPeriodos(DespesaPeriodoTecnicoParameters parameters) =>
            PagedList<DespesaPeriodo>.ToPagedList(_despesaPeriodoRepo.ObterPorParametros(
                new DespesaPeriodoParameters
                {
                    IndAtivo = parameters.IndAtivoPeriodo,
                    SortActive = parameters.SortActive,
                    SortDirection = parameters.SortDirection
                }), parameters.PageNumber, parameters.PageSize);

        private DespesaPeriodoTecnico ObterDespesaPeriodoTecnico(int codTecnico, int codPeriodo, string codDespesaPeriodoStatus) =>
            _despesaPeriodoTecnicoRepo.ObterPorParametros(
                new DespesaPeriodoTecnicoParameters
                {
                    CodTecnico = codTecnico,
                    CodDespesaPeriodo = codPeriodo,
                    IndAtivoPeriodo = 1
                }).SingleOrDefault();

        private List<DespesaAdiantamentoPeriodo> ObterDespesasPeriodoAdiantamentos(int codTecnico, int? codPeriodo = null) =>
            _despesaAdiantamentoPeriodoService.ObterDespesasPeriodoAdiantamentos(
                new DespesaAdiantamentoPeriodoParameters
                {
                    CodTecnico = codTecnico,
                    CodDespesaPeriodo = codPeriodo,
                    IndAdiantamentoAtivo = 1
                });

        private decimal TotalDespesa(DespesaPeriodoTecnico despesaPeriodo) =>
            despesaPeriodo != null ? despesaPeriodo.Despesas.Sum(d =>
                d.DespesaItens.Where(di =>
                    di.IndAtivo == 1 && di.CodDespesaTipo != 1 && di.CodDespesaTipo != 8).Sum(di =>
                        di.DespesaValor)) ?? 0 : 0;

        private decimal TotalAdiantamentoUtilizado(int codTecnico, int codPeriodo) =>
            this.ObterDespesasPeriodoAdiantamentos(codTecnico, codPeriodo).Sum(a => a.ValorAdiantamentoUtilizado);

        private List<DespesaPeriodoTecnicoAtendimentoItem> CalculaDespesasPorPeriodo(List<DespesaPeriodo> periodos, DespesaPeriodoTecnicoParameters parameters) =>
            periodos.Select(despesa =>
            {
                var despesaPeriodoTecnico =
                    this.ObterDespesaPeriodoTecnico(parameters.CodTecnico.Value, despesa.CodDespesaPeriodo, parameters.CodDespesaPeriodoStatus);

                return new DespesaPeriodoTecnicoAtendimentoItem
                {
                    CodDespesaPeriodo = despesa.CodDespesaPeriodo,
                    DataInicio = despesa.DataInicio,
                    DataFim = despesa.DataFim,
                    TotalDespesa = this.TotalDespesa(despesaPeriodoTecnico),
                    TotalAdiantamento = this.TotalAdiantamentoUtilizado(parameters.CodTecnico.Value, despesa.CodDespesaPeriodo),
                    GastosExcedentes = 0,
                    RestituirAEmpresa = 0,
                    Status = despesaPeriodoTecnico?.DespesaPeriodoTecnicoStatus,
                    IndAtivo = Convert.ToBoolean(despesa.IndAtivo)
                };
            }).ToList();

        public ListViewModel ObterAtendimentos(DespesaPeriodoTecnicoParameters parameters)
        {
            var despesasPeriodo = this.ObterPeriodos(parameters);
            var despesasPeriodoTecnico = this.CalculaDespesasPorPeriodo(despesasPeriodo, parameters);

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