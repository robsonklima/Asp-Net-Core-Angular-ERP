using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class NLogParameters : QueryStringParameters
    {
        public DateTime? DataRegistro { get; set; }
        public string Level { get; set; }
        public string Mensagem { get; set; }
    }
}
