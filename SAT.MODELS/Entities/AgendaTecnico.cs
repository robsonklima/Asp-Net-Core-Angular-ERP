using System;

namespace SAT.MODELS.Entities
{
    public class AgendaTecnico
    {
        public int CodAgendaTecnico { get; set; }
        public int? CodTecnico { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public string Tipo { get; set; }
        public DateTime? UltimaAtualizacao { get; set; }
        public int? CodOS { get; set; }
        public virtual OrdemServico OrdemServico { get; set; }
        public Tecnico Tecnico { get; set; }
    }
}