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
        public byte? IndDomingo { get; set; }
        public byte? IndSegunda { get; set; }
        public byte? IndTerca { get; set; }
        public byte? IndQuarta { get; set; }
        public byte? IndQuinta { get; set; }
        public byte? IndSexta { get; set; }
        public byte? IndSabado { get; set; }
    }
}