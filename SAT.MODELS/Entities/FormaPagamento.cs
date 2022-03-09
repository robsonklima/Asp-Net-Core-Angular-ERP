namespace SAT.MODELS.Entities
{
    public class FormaPagamento
    {
        public int CodFormaPagto { get; set; }
        public string DescFormaPagto { get; set; }
        public byte IndAtivo { get; set; }
        public decimal PercAjuste { get; set; }
    }
}
