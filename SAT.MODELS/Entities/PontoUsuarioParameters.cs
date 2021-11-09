using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class PontoUsuarioParameters : QueryStringParameters
    {
        public string CodUsuario { get; set; }
        public DateTime DataHoraRegistroInicio { get; set; }
        public DateTime DataHoraRegistroFim { get; set; }
    }
}
