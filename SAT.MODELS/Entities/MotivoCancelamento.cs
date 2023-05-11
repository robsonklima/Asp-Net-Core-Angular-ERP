using System;

namespace SAT.MODELS.Entities
{
    public class MotivoCancelamento
    {
        public int CodMotivoCancelamento { get; set; }
        public string Motivo { get; set; }
        public bool GeraNotaServico { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Pos { get; set; }
    }
}