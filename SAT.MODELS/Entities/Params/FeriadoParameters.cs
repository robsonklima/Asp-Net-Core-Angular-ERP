using SAT.MODELS.Entities.Helpers;
using System;

namespace SAT.MODELS.Entities.Params
{
    public class FeriadoParameters : QueryStringParameters
    {
        //public string CodFeriados { get; set; }
        public int? CodFeriado { get; set; }
        public DateTime? Mes { get; set; }
        public DateTime? dataInicio { get; set; }
        public DateTime? dataFim { get; set; }
        public string CodCidades { get; set; }
        public string CodUfs { get; set; }
    }
}
