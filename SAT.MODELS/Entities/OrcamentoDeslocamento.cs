using System;

namespace SAT.MODELS.Entities
{
    public class OrcamentoDeslocamento
    {
        public int CodOrcDeslocamento { get; set; }
        public int? CodOrc { get; set; }
        public decimal? QuantidadeHoraCadaSessentaKm { get; set; }
        public decimal? ValorUnitarioKmRodado { get; set; }
        public decimal? QuantidadeKm { get; set; }
        public decimal? ValorTotalKmRodado { get; set; }
        public decimal? ValorTotalKmDeslocamento { get; set; }
        public decimal? ValorHoraDeslocamento { get; set; }
        public decimal? LatitudeOrigem { get; set; }
        public decimal? LongitudeOrigem { get; set; }
        public decimal? LatitudeDestino { get; set; }
        public decimal? LongitudeDestino { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string UsuarioCadastro { get; set; }
    }
}