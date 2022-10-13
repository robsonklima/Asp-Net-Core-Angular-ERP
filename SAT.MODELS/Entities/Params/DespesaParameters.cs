using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class DespesaParameters : QueryStringParameters
    {
        public int? CodDespesaPeriodo { get; set; }
        public string CodTecnico { get; set; }
        public string CodRATs { get; set; }
        public byte? indAtivo { get; set; }
        public DateTime? DataHoraInicioRAT { get; set; }
        public DateTime? InicioPeriodo { get; set; }
        public DateTime? FimPeriodo { get; set; }
    }
}