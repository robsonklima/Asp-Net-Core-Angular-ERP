using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;
using System;

namespace SAT.MODELS.Entities.Params
{
    public class AgendaTecnicoParameters : QueryStringParameters
    {
        public string CodFiliais { get; set; }
        public string CodTecnicos { get; set; }
        public int? CodTecnico { get; set; }
        public string CodUsuario { get; set; }
        public int? CodOS { get; set; }
        public int? IndAtivo { get; set; }
        public AgendaTecnicoTypeEnum? Tipo { get; set; }
        public AgendaTecnicoOrdenationEnum? Ordenacao { get; set; }
        public DateTime? InicioPeriodoAgenda { get; set; }
        public DateTime? FimPeriodoAgenda { get; set; }
    }
}