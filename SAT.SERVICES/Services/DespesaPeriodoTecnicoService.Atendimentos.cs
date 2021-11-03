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
        private List<DespesaPeriodo> ObterDespesasPeriodo(DespesaPeriodoTecnicoParameters parameters) =>
            _despesaPeriodoRepo.ObterPorParametros(
                new DespesaPeriodoParameters
                {
                    IndAtivo = 1,
                    SortActive = parameters.SortActive,
                    SortDirection = parameters.SortDirection
                });

        private DespesaPeriodoTecnico ObterDespesaPeriodoTecnico(int codTecnico, int codPeriodo) =>
            _despesaPeriodoTecnicoRepo.ObterPorParametros(
                new DespesaPeriodoTecnicoParameters
                {
                    CodTecnico = codTecnico,
                    CodDespesaPeriodo = codPeriodo,
                    IndAtivoPeriodo = 1
                }).SingleOrDefault();

        private List<DespesaAdiantamentoPeriodo> ObterDespesasPeriodoAdiantamentos(int codTecnico, int codPeriodo) =>
            _despesaAdiantamentoPeriodoRepo.ObterPorParametros(
                new DespesaAdiantamentoPeriodoParameters
                {
                    CodTecnico = codTecnico,
                    CodDespesaPeriodo = codPeriodo,
                    IndAtivoPeriodo = 1
                });

        private decimal TotalDespesa(DespesaPeriodoTecnico despesaPeriodo) =>
            despesaPeriodo != null ? despesaPeriodo.Despesas.Sum(d =>
                d.DespesaItens.Where(di =>
                    di.IndAtivo == 1 && di.CodDespesaTipo != 1 && di.CodDespesaTipo != 8).Sum(di =>
                        di.DespesaValor)) ?? 0 : 0;

        private decimal TotalAdiantamentoUtilizado(int codTecnico, int codPeriodo) =>
            this.ObterDespesasPeriodoAdiantamentos(codTecnico, codPeriodo).Sum(a => a.ValorAdiantamentoUtilizado);

        private List<DespesaPeriodoTecnicoAtendimentoItem> CalculaDespesasPorPeriodo(DespesaPeriodoTecnicoParameters parameters) =>
            this.ObterDespesasPeriodo(parameters).Select(despesa =>
            {
                var despesaPeriodoTecnico =
                    this.ObterDespesaPeriodoTecnico(parameters.CodTecnico.Value, despesa.CodDespesaPeriodo);

                return new DespesaPeriodoTecnicoAtendimentoItem
                {
                    CodTecnico = parameters.CodTecnico.Value,
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

        public DespesaPeriodoTecnicoAtendimentoViewModel ObterAtendimentos(DespesaPeriodoTecnicoParameters parameters)
        {
            var despesasPeriodoTecnico =
                PagedList<DespesaPeriodoTecnicoAtendimentoItem>.ToPagedList(
                    CalculaDespesasPorPeriodo(parameters), parameters.PageNumber, parameters.PageSize);

            var lista = new DespesaPeriodoTecnicoAtendimentoViewModel
            {
                Items = despesasPeriodoTecnico,
                TotalCount = despesasPeriodoTecnico.TotalCount,
                CurrentPage = despesasPeriodoTecnico.CurrentPage,
                PageSize = despesasPeriodoTecnico.PageSize,
                TotalPages = despesasPeriodoTecnico.TotalPages,
                HasNext = despesasPeriodoTecnico.HasNext,
                HasPrevious = despesasPeriodoTecnico.HasPrevious
            };

            return lista;
        }
    }
}