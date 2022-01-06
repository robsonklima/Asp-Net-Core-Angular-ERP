using System;
using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities
{
    public class MonitoramentoParameters : QueryStringParameters
    {
        public MonitoramentoTipoEnum Tipo { get; set; }
    }
}