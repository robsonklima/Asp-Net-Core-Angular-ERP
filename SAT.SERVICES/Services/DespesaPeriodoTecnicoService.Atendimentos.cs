using System.Collections.Generic;
using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class DespesaPeriodoTecnicoService : IDespesaPeriodoTecnicoService
    {
        public List<DespesaPeriodo> ObterDespesasPeriodo(DespesaPeriodoTecnicoParameters parameters) =>
            _despesaPeriodoRepo.ObterPorParametros(
                new DespesaPeriodoParameters
                {
                    IndAtivo = 1,
                    SortActive = parameters.SortActive,
                    SortDirection = parameters.SortDirection
                });

        public DespesaPeriodoTecnico ObterDespesaPeriodoTecnico(int codTecnico, int codPeriodo) =>
            _despesaPeriodoTecnicoRepo.ObterPorParametros(
                new DespesaPeriodoTecnicoParameters
                {
                    CodTecnico = codTecnico,
                    CodDespesaPeriodo = codPeriodo,
                    IndAtivoPeriodo = 1
                }).SingleOrDefault();

        public List<DespesaAdiantamentoPeriodo> ObterDespesasPeriodoAdiantamentos(int codTecnico, int codPeriodo) =>
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


        private decimal TotalAdiantamento(int codTecnico, int codPeriodo)
        {
            var adiantamentos = this.ObterDespesasPeriodoAdiantamentos(codTecnico, codPeriodo);

            var valorAdiantamento = adiantamentos.Sum(a => a.DespesaAdiantamento.ValorAdiantamento);
            var valorUtilizado = adiantamentos.Sum(a => a.ValorAdiantamentoUtilizado);
            var valorSaldo = valorAdiantamento - valorUtilizado;

            return valorSaldo;
        }

        private decimal TotalAdiantamentoUtilizado(int codTecnico, int codPeriodo) =>
            this.ObterDespesasPeriodoAdiantamentos(codTecnico, codPeriodo).Sum(a => a.ValorAdiantamentoUtilizado);

        public List<DespesaPeriodoTecnicoViewModel> GetDespesaPeriodoViewModel(DespesaPeriodoTecnicoParameters parameters) =>
            this.ObterDespesasPeriodo(parameters).Select(despesa =>
            {
                var despesaPeriodoTecnico =
                    this.ObterDespesaPeriodoTecnico(parameters.CodTecnico.Value, despesa.CodDespesaPeriodo);

                return new DespesaPeriodoTecnicoViewModel
                {
                    CodTecnico = parameters.CodTecnico.Value,
                    DataInicio = despesa.DataInicio,
                    DataFim = despesa.DataFim,
                    TotalDespesa = this.TotalDespesa(despesaPeriodoTecnico),
                    TotalAdiantamento = this.TotalAdiantamentoUtilizado(parameters.CodTecnico.Value, despesa.CodDespesaPeriodo),
                    GastosExcedentes = 0,
                    RestituirAEmpresa = 0,
                    Status = despesaPeriodoTecnico?.DespesaPeriodoTecnicoStatus
                };
            }).ToList();
    }
}