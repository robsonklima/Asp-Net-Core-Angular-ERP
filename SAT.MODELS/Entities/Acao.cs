namespace SAT.MODELS.Entities
{
    public class Acao
    {
        public int CodAcao { get; set; }
        public string CodEAcao { get; set; }
        public string NomeAcao { get; set; }
        public byte? IndPeca { get; set; }
        public byte? IndAtivo { get; set; }
        public int? CodTraducao { get; set; }
        public int? CodStatusServico { get; set; }
    }
}
