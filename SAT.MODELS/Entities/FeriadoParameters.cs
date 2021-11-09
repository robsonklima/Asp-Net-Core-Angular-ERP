using SAT.MODELS.Entities.Helpers;
using System;

namespace SAT.MODELS.Entities
{
    public class FeriadoParameters : QueryStringParameters
    {
        public int? CodFeriado { get; set; }
        public DateTime? Mes { get; set; }
    }
}
