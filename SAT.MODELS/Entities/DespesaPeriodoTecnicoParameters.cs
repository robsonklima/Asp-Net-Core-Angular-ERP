using System;
using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities
{
    public class DespesaPeriodoTecnicoParameters : QueryStringParameters
    {
        public string CodTecnico { get; set; }
        public string CodFilial { get; set; }
        public int? CodDespesaPeriodo { get; set; }
        public string CodDespesaProtocolo { get; set; }
        public int? IndAtivoPeriodo { get; set; }
        public string CodDespesaPeriodoStatus { get; set; }
        public DateTime? InicioPeriodo { get; set; }
        public DateTime? FimPeriodo { get; set; }
        public DespesaPeriodoTecnicoFilterEnum FilterType { get; set; }
        public Boolean ValidRequest { get; set; }
    }
}