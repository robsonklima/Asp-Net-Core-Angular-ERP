using System;

namespace SAT.MODELS.Entities
{
    public class SATFeriado
    {
        public int CodSATFeriado { get; set; }
        public string Data { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public string UF { get; set; }
        public string Municipio { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
    }
}
