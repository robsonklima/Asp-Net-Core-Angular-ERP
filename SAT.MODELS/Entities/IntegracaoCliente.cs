using System;
using System.Text.Json.Serialization;

namespace SAT.MODELS.Entities
{
    public class IntegracaoCliente
    {
        public string Chave { get; set; }
        public string NumIncidenteCliente { get; set; }
        public string NumIncidentePerto { get; set; }
        public string RelatoCliente { get; set; }
        public string NumSerie { get; set; }
        public string NumAgencia { get; set; }
        public string Observacao { get; set; }
        public DateTime? DataHoraAberturaPerto { get; set; }
        [JsonIgnore]
        public EquipamentoCliente Equipamento { get; set; }
    }
}