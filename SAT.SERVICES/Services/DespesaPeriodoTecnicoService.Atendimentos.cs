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
        private PagedList<DespesaPeriodo> ObterDespesasPeriodo(DespesaPeriodoTecnicoParameters parameters) =>
            PagedList<DespesaPeriodo>.ToPagedList(_despesaPeriodoRepo.ObterPorParametros(
                new DespesaPeriodoParameters
                {
                    IndAtivo = 1,
                    SortActive = parameters.SortActive,
                    SortDirection = parameters.SortDirection
                }), parameters.PageNumber, parameters.PageSize);

        private DespesaPeriodoTecnico ObterDespesaPeriodoTecnico(int codTecnico, int codPeriodo) =>
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

        private List<DespesaPeriodoTecnicoAtendimentoItem> CalculaDespesasPorPeriodo(List<DespesaPeriodo> periodos, int codTecnico) =>
            periodos.Select(despesa =>
            {
                var despesaPeriodoTecnico =
                    this.ObterDespesaPeriodoTecnico(codTecnico, despesa.CodDespesaPeriodo);

                return new DespesaPeriodoTecnicoAtendimentoItem
                {
                    CodDespesaPeriodo = despesa.CodDespesaPeriodo,
                    DataInicio = despesa.DataInicio,
                    DataFim = despesa.DataFim,
                    TotalDespesa = this.TotalDespesa(despesaPeriodoTecnico),
                    TotalAdiantamento = this.TotalAdiantamentoUtilizado(codTecnico, despesa.CodDespesaPeriodo),
                    GastosExcedentes = 0,
                    RestituirAEmpresa = 0,
                    Status = despesaPeriodoTecnico?.DespesaPeriodoTecnicoStatus,
                    IndAtivo = Convert.ToBoolean(despesa.IndAtivo)
                };
            }).ToList();

        public ListViewModel ObterAtendimentos(DespesaPeriodoTecnicoParameters parameters)
        {
            var despesasPeriodo = this.ObterDespesasPeriodo(parameters);
            var despesasPeriodoTecnico = this.CalculaDespesasPorPeriodo(despesasPeriodo, parameters.CodTecnico.Value);

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