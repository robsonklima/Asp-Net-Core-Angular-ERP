using System;
using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities.Params
{
    public class DespesaPeriodoTecnicoParameters : QueryStringParameters
    {
        public string CodTecnico { get; set; }
        public int? CodDespesaPeriodoTecnico { get; set; }
        public string CodFilial { get; set; }
        public int? CodDespesaPeriodo { get; set; }
        public string CodDespesaProtocolo { get; set; }
        public int? IndAtivoPeriodo { get; set; }
        public DateTime? InicioPeriodo { get; set; }
        public DateTime? FimPeriodo { get; set; }
        public DespesaPeriodoTecnicoFilterEnum FilterType { get; set; }
        public DespesaCreditoCartaoStatusEnum? CodCreditoCartaoStatus { get; set; }
        public string CodDespesaPeriodoStatusNotIn { get; set; }
        public string CodDespesaPeriodoTecnicoStatus { get; set; }
        public bool EstaEmProtocolo { get; set; }
    }
}