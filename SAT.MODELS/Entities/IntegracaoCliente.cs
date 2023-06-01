using System;
using System.Text.Json.Serialization;
using SAT.MODELS.Entities;

namespace SAT.MODELS.Entities
{
    public class IntegracaoCliente
    {
        public string Chave { get; set; }
        public string NumIncidenteCliente { get; set; }
        public string NumIncidentePerto { get; set; }
        public string RelatoCliente { get; set; }
        public string NumSerie { get; set; }
        public EquipamentoCliente Equipamento { get; set; }
    }
}