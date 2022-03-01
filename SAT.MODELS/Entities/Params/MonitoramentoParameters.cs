using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class MonitoramentoParameters : QueryStringParameters
    {
        public string Tipo { get; set; }
        public string Item { get; set; }
        public string Servidor { get; set; }
        public DateTime? DataHoraProcessamento { get; set; }
    }
}