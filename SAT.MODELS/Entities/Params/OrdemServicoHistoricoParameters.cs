using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class OrdemServicoHistoricoParameters : QueryStringParameters
    {
        public int? CodOS { get; set; }
        public DateTime? DataHoraCadInicio { get; set; }
        public DateTime? DataHoraCadFim { get; set; }
    }
}
