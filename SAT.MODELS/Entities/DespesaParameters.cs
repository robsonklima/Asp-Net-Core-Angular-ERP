using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class DespesaParameters : QueryStringParameters
    {
        public int? CodDespesaPeriodo { get; set; }
        public string CodTecnico { get; set; }
        public string CodRATs { get; set; }
        public DateTime? DataHoraInicioRAT { get; set; }
    }
}