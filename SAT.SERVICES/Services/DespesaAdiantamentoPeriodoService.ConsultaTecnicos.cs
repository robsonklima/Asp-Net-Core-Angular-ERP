using System;
using System.Collections.Generic;
using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class DespesaAdiantamentoPeriodoService : IDespesaAdiantamentoPeriodoService
    {
        public DespesaPeriodoTecnico ObterUltimaDespesaPeriodoTecnico(int codTecnico) =>
            _despesaPeriodoTecnicoRepo.ObterPorParametros(
                new DespesaPeriodoTecnicoParameters
                {
                    CodTecnico = codTecnico,
                    SortActive = "CodDespesaPeriodo",
                    SortDirection = "desc",
                    IndAtivoPeriodo = 1,
                    PageSize = 5
                }).FirstOrDefault();

        public List<DespesaAdiantamentoPeriodo> ObterDespesasPeriodoAdiantamentos(DespesaAdiantamentoPeriodoParameters parameters) =>
            _despesaAdiantamentoPeriodoRepo.ObterPorParametros(
                new DespesaAdiantamentoPeriodoParameters
                {
                    CodTecnico = parameters.CodTecnico,
                    CodDespesaPeriodo = parameters.CodDespesaPeriodo,
                    IndAdiantamentoAtivo = parameters.IndAdiantamentoAtivo
                });

        public List<DespesaAdiantamento> ObterDespesasAdiantamento(DespesaAdiantamentoParameters parameters) =>
            _despesaAdiantamentoRepo.ObterPorParametros(
                new DespesaAdiantamentoParameters
                {
                    CodTecnico = parameters.CodTecnico,
                    IndAtivo = parameters.IndAtivo
                });


        private List<Tecnico> ObterTecnicos(DespesaAdiantamentoPeriodoParameters parameters) =>
            _tecnicoRepo.ObterPorParametros(
                new TecnicoParameters
                {
                    IndAtivo = parameters.IndAtivoTecnico,
                    SortActive = parameters.SortActive,
                    SortDirection = parameters.SortDirection
                });

        private decimal SaldoAdiantamento(int codTecnico)
        {
            var adiantamentosPeriodo =
                this.ObterDespesasPeriodoAdiantamentos(new DespesaAdiantamentoPeriodoParameters
                {
                    CodTecnico = codTecnico,
                    IndAdiantamentoAtivo = 1
                });

            var adiantamentos =
                this.ObterDespesasAdiantamento(new DespesaAdiantamentoParameters
                {
                    CodTecnico = codTecnico,
                    IndAtivo = 1
                });

            var valorAdiantamento =
                adiantamentos.Sum(a => a.ValorAdiantamento);

            var valorUtilizado = adiantamentosPeriodo
                .Where(a => a.DespesaAdiantamento.CodDespesaAdiantamentoTipo == 2 && a.DespesaAdiantamento.IndAtivo == 1)
                .Sum(a => a.ValorAdiantamentoUtilizado);

            var valorSaldo = valorAdiantamento - valorUtilizado;

            return valorSaldo;
        }

        private bool IsLiberado(int codTecnico)
        {
            var ultimaDespesa = this.ObterUltimaDespesaPeriodoTecnico(codTecnico);

            if (ultimaDespesa == null || ultimaDespesa.CodDespesaPeriodoTecnicoStatus != 1) return false;

            return true;
        }

        private List<DespesaAdiantamentoPeriodoConsultaTecnicoItem> CalculaAdiantamentoPorTecnico(DespesaAdiantamentoPeriodoParameters parameters) =>
         this.ObterTecnicos(parameters)
             .Select(tecnico =>
            {
                return new DespesaAdiantamentoPeriodoConsultaTecnicoItem
                {
                    Tecnico = tecnico,
                    SaldoAdiantamento = this.SaldoAdiantamento(tecnico.CodTecnico),
                    Liberado = this.IsLiberado(tecnico.CodTecnico),
                    IndAtivo = Convert.ToBoolean(tecnico.IndAtivo)
                };
            }).ToList();


        public DespesaAdiantamentoPeriodoConsultaTecnicoViewModel ObterConsultaTecnicos(DespesaAdiantamentoPeriodoParameters parameters)
        {
            var consultaTecnicos =
               PagedList<DespesaAdiantamentoPeriodoConsultaTecnicoItem>.ToPagedList(
                   CalculaAdiantamentoPorTecnico(parameters), parameters.PageNumber, parameters.PageSize);

            var lista = new DespesaAdiantamentoPeriodoConsultaTecnicoViewModel
            {
                Items = consultaTecnicos,
                TotalCount = consultaTecnicos.TotalCount,
                CurrentPage = consultaTecnicos.CurrentPage,
                PageSize = consultaTecnicos.PageSize,
                TotalPages = consultaTecnicos.TotalPages,
                HasNext = consultaTecnicos.HasNext,
                HasPrevious = consultaTecnicos.HasPrevious
            };

            return lista;
        }
    }
}