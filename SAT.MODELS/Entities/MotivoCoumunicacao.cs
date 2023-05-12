using System;

namespace SAT.MODELS.Entities
{
    public class MotivoComunicacao
    {
        public int CodMotivoComunicacao { get; set; }
        public string Motivo { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}