using System;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities
{
    public class AgendaTecnico
    {
        public int? CodAgendaTecnico { get; set; }
        public int? CodTecnico { get; set; }
        public string Titulo { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public AgendaTecnicoTipoEnum Tipo { get; set; }
        public int IndAtivo { get; set; }
        public int? CodOS { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraCad { get; set; }
    }
}