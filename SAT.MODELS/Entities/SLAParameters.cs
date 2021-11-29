using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class SLAParameters : QueryStringParameters
    {
        public string NomeSLA { get; set; }
        public int? TempoInicio { get; set; }
        public int? TempoReparo { get; set; }
        public int? TempoSolucao { get; set; }
    }
}
