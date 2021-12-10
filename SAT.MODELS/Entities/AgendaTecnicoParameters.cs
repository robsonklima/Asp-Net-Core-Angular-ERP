using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;
using System;

namespace SAT.MODELS.Entities
{
    public class AgendaTecnicoParameters : QueryStringParameters
    {
        public string CodFiliais { get; set; }
        public string CodTecnicos { get; set; }
        public int? CodTecnico { get; set; }
        public AgendaTecnicoTypeEnum? Tipo { get; set; }
        public DateTime? InicioPeriodoAgenda { get; set; }
        public DateTime? FimPeriodoAgenda { get; set; }
    }
}