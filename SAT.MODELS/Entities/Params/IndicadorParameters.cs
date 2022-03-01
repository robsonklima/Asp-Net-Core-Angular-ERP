using SAT.MODELS.Enums;
using System;

namespace SAT.MODELS.Entities.Params
{
    public class IndicadorParameters
    {
        public IndicadorAgrupadorEnum Agrupador { get; set; }
        public OrdemServicoIncludeEnum Include { get; set; }
        public OrdemServicoFilterEnum FilterType { get; set; }
        public IndicadorTipoEnum Tipo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string CodClientes { get; set; }
        public string CodFiliais { get; set; }
        public string CodStatusServicos { get; set; }
        public string CodTiposIntervencao { get; set; }
        public string CodAutorizadas { get; set; }
        public string CodTiposGrupo { get; set; }
        public string CodMagnus { get; set; }
    }
}
