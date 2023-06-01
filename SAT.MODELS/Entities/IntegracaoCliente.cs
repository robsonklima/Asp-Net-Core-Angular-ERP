using System;
using System.Text.Json.Serialization;

namespace SAT.MODELS.Entities
{
    public class IntegracaoCliente
    {
        public string numIncidenteCliente { get; set; }
        public string NumIndidentePerto { get; set; }
        public string RelatoCliente { get; set; }
        [JsonIgnore]
        public string Chave { get; set; }
        public string NumSerie { get; set; }
    }
}