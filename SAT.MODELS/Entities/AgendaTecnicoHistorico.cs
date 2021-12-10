using System;

namespace SAT.MODELS.Entities
{
    public class AgendaTecnicoHistorico
    {
        public int CodAgendaTecnicoHistorico { get; set; }
        public int CodAgendaTecnico { get; set; }
        public int CodTecnico { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public int IsAgendamento { get; set; }
        public DateTime? UltimaAtualizacao { get; set; }
    }
}