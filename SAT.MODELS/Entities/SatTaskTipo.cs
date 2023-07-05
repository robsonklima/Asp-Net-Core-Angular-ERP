using System;

namespace SAT.MODELS.Entities
{
    public class SatTaskTipo
    {
        public int CodSatTaskTipo { get; set; }
        public string Nome { get; set; }
        public byte IndAtivo { get; set; }
        public byte IndProcesso { get; set; }
        public int TempoRepeticaoMinutos { get; set; }
        public TimeSpan Inicio { get; set; }
        public TimeSpan Fim { get; set; }
        public int? DiaSemanaInicio { get; set; }
        public int? DiaSemanaFim { get; set; }
    }
}