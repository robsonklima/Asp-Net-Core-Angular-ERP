namespace SAT.MODELS.ViewModels {
    public class ViewIntegracaoFinanceiroOrcamentoItem
    {
        public int CodOrc { get; set; }
        public int TipoFaturamento { get; set; }
        public int CodItem { get; set; }
        public int Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public string PedidoCliente { get; set; }
        public int SeqItemPedido { get; set; }
    }
}