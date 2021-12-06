using SAT.MODELS.Entities.Helpers;
using System;

namespace SAT.MODELS.Entities
{
    public class AgendaTecnicoParameters : QueryStringParameters
    {
        public int? PA { get; set; }
        public string CodFiliais { get; set; }
        public int? CodOS { get; set; }
        public int? CodTecnico { get; set; }
        public string Tipo { get; set; }
        public DateTime? InicioPeriodoAgenda { get; set; }
        public DateTime? FimPeriodoAgenda { get; set; }
    }
}