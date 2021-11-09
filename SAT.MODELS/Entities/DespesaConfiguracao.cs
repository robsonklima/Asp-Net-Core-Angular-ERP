using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class DespesaConfiguracao
    {
        [Key]
        public int CodDespesaConfiguracao { get; set; }
        public decimal PercentualKmCidade { get; set; }
        public decimal PercentualKmForaCidade { get; set; }
        public decimal ValorRefeicaoLimiteTecnico { get; set; }
        public decimal ValorRefeicaoLimiteOutros { get; set; }
        public DateTime HoraExtraInicioAlmoco { get; set; }
        public DateTime HoraExtraInicioJanta { get; set; }
        public decimal PercentualNotaKM { get; set; }
        public decimal ValorKM { get; set; }
        public decimal? ValorAluguelCarro { get; set; }
        public DateTime DataVigencia { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}