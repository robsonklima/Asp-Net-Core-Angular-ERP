using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class UsuarioLoginParameters : QueryStringParameters
    {
        public DateTime? DataHoraCadInicio { get; set; }
        public DateTime? DataHoraCadFim { get; set; }
        public string CodUsuario { get; set; }
    }
}
