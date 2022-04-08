using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;
using System;

namespace SAT.MODELS.Entities.Params
{
    public class AgendaTecnicoParameters : QueryStringParameters
    {
        public int CodFilial { get; set; }
        public string CodTecnicos { get; set; }
        public string PAS { get; set; }
        public int CodOS { get; set; }
        public int? IndAtivo { get; set; }
        public AgendaTecnicoTipoEnum? Tipo { get; set; }
        public int IndFerias { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }
    }
}