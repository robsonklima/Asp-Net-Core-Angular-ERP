using System;
using System.Text.Json.Serialization;

namespace SAT.MODELS.Entities
{
    public class Integracao
    {
        [JsonIgnore]
        public int CodOSIntegracao { get; set; }
        [JsonIgnore]
        public int? CodOS { get; set; }
        public int CodCliente { get; set; }
        public string NomeSolicitante { get; set; }
        public string TelefoneSolicitante { get; set; }
        public string NumSerie { get; set; }
        public string NumOSCliente { get; set; }
        public string DefeitoRelatado { get; set; }
        public DateTime? DataHoraSolicitacao { get; set; }
        [JsonIgnore]
        public byte? IndProcessamento { get; set; }
        [JsonIgnore]
        public DateTime? DataHoraProcessamento { get; set; }
    }
}