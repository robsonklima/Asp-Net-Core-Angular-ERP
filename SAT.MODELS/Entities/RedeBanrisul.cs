namespace SAT.MODELS.Entities
{
    public class RedeBanrisul
    {
        public int CodRedeBanrisul { get; set; }
        public string Rede { get; set; }
        public bool Ativo { get; set; }
        public bool? FaturaProduto { get; set; }
        public bool? FaturaServico { get; set; }
    }
}
