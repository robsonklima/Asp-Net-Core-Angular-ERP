using System;

namespace SAT.MODELS.Entities
{
    public class VersaoAlteracao
    {
        public int CodSatVersaoAlteracao { get; set; }
        public int CodSatVersao { get; set; }
        public int CodSatVersaoAlteracaoTipo { get; set; }
        public string Nome { get; set; }
        public virtual VersaoAlteracaoTipo Tipo { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}
