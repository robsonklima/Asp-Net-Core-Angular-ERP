using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.MODELS.Views;

namespace SAT.MODELS.ViewModels
{
    public class DespesaPeriodoTecnicoImpressaoModel
    {
        public decimal AluguelCarro { get; set; }
        public decimal Correio { get; set; }
        public decimal Frete { get; set; }
        public decimal Outros { get; set; }
        public decimal Pedagio { get; set; }
        public decimal Taxi { get; set; }
        public decimal CartaoTelefonico { get; set; }
        public decimal Estacionamento { get; set; }
        public decimal Hotel { get; set; }
        public decimal PassagemAerea { get; set; }
        public decimal CartaoCombustivel { get; set; }
        public decimal Telefone { get; set; }
        public decimal Combustivel { get; set; }
        public decimal Ferramentas { get; set; }
        public decimal Onibus { get; set; }
        public decimal PecasComponentes { get; set; }
        public decimal Refeicao { get; set; }
        public decimal Internet { get; set; }
        public decimal DespesaKM { get; set; }
        public decimal DespesaOutros { get; set; }
        public decimal TotalDespesa { get; set; }
        public decimal AdiantamentoRecebido { get; set; }
        public decimal AdiantamentoUtilizado { get; set; }
        public decimal AReceberViaDeposito { get; set; }
        public decimal SaldoAdiantamento { get; set; }
        public decimal PercentualOutros { get; set; }
        public decimal PercentualDespesaCB { get; set; }
        public List<DespesaAdiantamentoPeriodo> Adiantamentos { get; set; }
        public List<ViewDespesaImpressaoItem> Itens { get; set; }
        public DespesaPeriodoTecnico Despesa { get; set; }
    }
}
