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
        private List<DespesaPeriodoTecnicoAtendimentoItem> CalculaAdiantamentoPorTecnico(DespesaPeriodoTecnicoParameters parameters)
        {
            return null;
        }

        private decimal SaldoAdiantamento(int codTecnico, int codPeriodo)
        {
            var adiantamentos = this.ObterDespesasPeriodoAdiantamentos(codTecnico, codPeriodo);

            var valorAdiantamento = adiantamentos.Sum(a => a.DespesaAdiantamento.ValorAdiantamento);
            var valorUtilizado = adiantamentos.Sum(a => a.ValorAdiantamentoUtilizado);
            var valorSaldo = valorAdiantamento - valorUtilizado;

            return valorSaldo;
        }

        public DespesaPeriodoTecnicoAtendimentoViewModel ObterTecnicos(DespesaPeriodoTecnicoParameters parameters)
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