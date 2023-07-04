namespace SAT.MODELS.Entities
{
    public class SatTaskTipo
    {
        public int CodSatTaskTipo { get; set; }
        public string Nome { get; set; }
        public byte IndAtivo { get; set; }
        public byte IndProcesso { get; set; }
        public int TempoRepeticaoMinutos { get; set; }
    }
}