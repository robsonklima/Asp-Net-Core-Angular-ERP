using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class PontoUsuarioParameters : QueryStringParameters
    {
        public string CodUsuario { get; set; }
        public string CodUsuarios { get; set; }
        public int? CodPontoPeriodo { get; set; }
        public int? IndAtivo { get; set; }
        public DateTime DataHoraRegistroInicio { get; set; }
        public DateTime DataHoraRegistroFim { get; set; }
        public DateTime? DataHoraRegistro { get; set; }
    }
}
