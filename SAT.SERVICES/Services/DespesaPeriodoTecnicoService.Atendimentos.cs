using System.Collections.Generic;
using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class DespesaPeriodoTecnicoService : IDespesaPeriodoTecnicoService
    {

        public List<DespesaPeriodo> ObterDespesasPeriodo()
        {
            var periodos = _despesaPeriodoRepo.ObterPorParametros(
                new DespesaPeriodoParameters
                {
                    IndAtivo = 1
                });

            return periodos;
        }

        public DespesaPeriodoTecnico ObterDespesaPeriodoTecnico(int codTecnico, int codPeriodo)
        {
            var despesas = _despesaPeriodoTecnicoRepo.ObterPorParametros(
                new DespesaPeriodoTecnicoParameters
                {
                    CodTecnico = codTecnico,
                    CodDespesaPeriodo = 636,
                    IndAtivoPeriodo = 1
                });

            return despesas.SingleOrDefault();
        }

        public List<DespesaAdiantamentoPeriodo> ObterDespesasPeriodoAdiantamentos(int codTecnico, int codPeriodo)
        {
            var adiantamentos = _despesaAdiantamentoPeriodoRepo.ObterPorParametros(
                new DespesaAdiantamentoPeriodoParameters
                {
                    CodTecnico = codTecnico,
                    CodDespesaPeriodo = codPeriodo,
                    IndAtivoPeriodo = 1
                });

            return adiantamentos;
        }


        private decimal TotalDespesa(DespesaPeriodoTecnico despesaPeriodo)
        {
            return despesaPeriodo != null ? despesaPeriodo.Despesas.Sum(d =>
                            d.DespesaItens.Where(di =>
                                di.CodDespesaTipo != 1 && di.CodDespesaTipo != 8).Sum(di =>
                                    di.DespesaValor)) ?? 0 : 0;
        }

        private decimal TotalAdiantamento(int codTecnico, int codPeriodo)
        {
            var adiantamentos = this.ObterDespesasPeriodoAdiantamentos(codTecnico, codPeriodo);

            var valorAdiantamento = adiantamentos.Sum(a => a.DespesaAdiantamento.ValorAdiantamento);
            var valorUtilizado = adiantamentos.Sum(a => a.ValorAdiantamentoUtilizado);
            var valorSaldo = valorAdiantamento - valorUtilizado;

            return valorSaldo;
        }

        public DespesaPeriodoViewModel GetDespesaPeriodoViewModel(DespesaPeriodoTecnicoParameters parameters)
        {
            var model = new DespesaPeriodoViewModel
            {
                Items = new List<DespesaPeriodoTecnicoViewModel>()
            };

            var periodos = this.ObterDespesasPeriodo();

            periodos.ForEach(despesa =>
            {
                var despesaPeriodoTecnico = this.ObterDespesaPeriodoTecnico(parameters.CodTecnico.Value, despesa.CodDespesaPeriodo);

                var despesaPeriodoTecnicoModel = new DespesaPeriodoTecnicoViewModel
                {
                    CodTecnico = parameters.CodTecnico.Value,
                    DataInicio = despesa.DataInicio,
                    DataFim = despesa.DataFim,
                    TotalDespesa = this.TotalDespesa(despesaPeriodoTecnico),
                    TotalAdiantamento = this.TotalAdiantamento(parameters.CodTecnico.Value, despesa.CodDespesaPeriodo),
                    GastosExcedentes = 0,
                    RestituirAEmpresa = 0,
                    Status = despesaPeriodoTecnico?.DespesaPeriodoTecnicoStatus
                };

                model.Items.Add(despesaPeriodoTecnicoModel);
            });

            return model;
        }
    }
}