using System;

namespace SAT.MODELS.Entities
{
    public class MtbfEquipamento
    {
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public double Resultado { get; set; }
        public int? QtdOS { get; set; }
    }
}
