using System;

namespace SAT.MODELS.Entities
{
    public class IndicadorParameters
    {
        public string Agrupador { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string CodClientes { get; set; }
        public string CodFiliais { get; set; }
    }
}
