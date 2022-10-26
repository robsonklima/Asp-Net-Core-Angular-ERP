using System;
using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class OR
    {
        public int CodOR { get; set; }
        public DateTime DataHoraOR { get; set; }
        public int? CodOrigem { get; set; }
        public int? CodDestino { get; set; }
        public ORDestino Destino { get; set; }
        public int CodStatusOR { get; set; }
        public ORStatus ORStatus { get; set; }
        public string NumNF { get; set; }
        public string Volumes { get; set; }
        public int? CodModal { get; set; }
        public DateTime DataExpedicao { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public Usuario UsuarioCadastro { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string Minuta { get; set; }
        public int? CodTransportadora { get; set; }
        public List<ORItem> ORItens { get; set; }
    }
}