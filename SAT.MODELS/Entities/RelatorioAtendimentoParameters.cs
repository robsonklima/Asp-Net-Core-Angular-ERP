using System;
using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities
{
    public class RelatorioAtendimentoParameters : QueryStringParameters
    {
        public int? CodRAT { get; set; }
        public int? CodOS { get; set; }
        public string CodTecnicos { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataSolucao { get; set; }
        public RelatorioAtendimentoIncludeEnum Include { get; set; }
    }
}
