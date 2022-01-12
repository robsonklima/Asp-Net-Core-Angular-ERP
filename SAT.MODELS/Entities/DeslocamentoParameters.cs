using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class DeslocamentoParameters : QueryStringParameters
    {
        public int? CodTecnico { get; set; }
        public DateTime? DataHoraInicioInicio { get; set; }
        public DateTime? DataHoraInicioFim { get; set; }
    }
}
