using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class MonitoramentoParameters : QueryStringParameters
    {
        public string Tipo { get; set; }
        public string Item { get; set; }
        public string Servidor { get; set; }
        public DateTime? DataHoraProcessamentoInicio { get; set; }
        public DateTime? DataHoraProcessamentoFim { get; set; }
    }
}