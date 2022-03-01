using SAT.MODELS.Enums;
using System;

namespace SAT.MODELS.Entities.Params
{
    public class DashboardParameters
    {
        public DashboardAgrupadorEnum Agrupador { get; set; }
        public DashboardIncludeEnum Include { get; set; }
        public DashboardFilterEnum FilterType { get; set; }
        public DashboardTipoEnum Tipo { get; set; }

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
