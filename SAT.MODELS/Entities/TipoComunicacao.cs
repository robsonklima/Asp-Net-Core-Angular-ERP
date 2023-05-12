using System;

namespace SAT.MODELS.Entities
{
    public class TipoComunicacao

    {
        public int CodTipoComunicacao { get; set; }
        public string Tipo { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
