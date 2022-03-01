using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;
using System;

namespace SAT.MODELS.Entities.Params
{
    public class FilialParameters : QueryStringParameters
    {
        public int? CodFilial { get; set; }
        public string CodFiliais { get; set; }
        public int? IndAtivo { get; set; }
        public string SiglaUF { get; set; }
        public DateTime? periodoInicioAtendendimento { get; set; }
        public DateTime? periodoFimAtendendimento { get; set; }
        public FilialIncludeEnum Include { get; set; }
        public FilialFilterEnum FilterType { get; set; }
    }
}
