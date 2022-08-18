namespace SAT.MODELS.Entities
{
    public class OrcamentoFaturamento
    {
        public int? CodOrcamentoFaturamento { get; set; }
        public int? CodOrcamento { get; set; }
        public string CodClienteBancada { get; set; }
        public int? CodFilial { get; set; }
        public double? NumOSPerto { get; set; }
        public string NumOrcamento { get; set; }
        public string DescricaoNotaFiscal { get; set; }
        public string ValorPeca { get; set; }
        public double? QtdePeca { get; set; }
        public string ValorServico { get; set; }
        public double? NumNF { get; set; }
        public string DataEmissaoNF { get; set; }
        public double? IndFaturado { get; set; }
        public string IndRegistroDanfe { get; set; }
        public string CaminhoDanfe { get; set; }
        public string CodUsuarioCad { get; set; }
        public string DataHoraCad { get; set; }
    }
}