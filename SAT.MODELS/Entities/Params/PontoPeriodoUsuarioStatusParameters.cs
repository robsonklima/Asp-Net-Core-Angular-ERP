using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class PontoPeriodoUsuarioStatusParameters : QueryStringParameters
    {
        public int? CodPontoPeriodoUsuarioStatus { get; set; }
        public string Descricao { get; set; }
    }
}
