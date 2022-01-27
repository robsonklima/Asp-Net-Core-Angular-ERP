using System;
using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class Versao
    {
        public int CodSatVersao { get; set; }
        public string Nome { get; set; }
        public virtual List<VersaoAlteracao> Alteracoes { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}
