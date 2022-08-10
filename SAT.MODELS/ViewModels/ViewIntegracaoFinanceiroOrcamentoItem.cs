namespace SAT.MODELS.ViewModels {
    public class ViewIntegracaoFinanceiroOrcamentoItem
    {
        public int CodOrc { get; set; }
        public int TipoFaturamento { get; set; }
        public string CodItem { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public string PedidoCliente { get; set; }
        public decimal ValorTotal  { get; set; }
        public decimal ValorDesconto { get; set; }
        public int SeqItemPedido { get; set; }
    }
}