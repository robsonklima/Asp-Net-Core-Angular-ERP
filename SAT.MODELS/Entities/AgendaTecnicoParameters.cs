using SAT.MODELS.Entities.Helpers;
using System;

namespace SAT.MODELS.Entities
{
    public class AgendaTecnicoParameters : QueryStringParameters
    {
        public int? PA { get; set; }
        public string CodFiliais { get; set; }
        public int? CodOS { get; set; }
        public int? CodTecnico { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }
    }
}