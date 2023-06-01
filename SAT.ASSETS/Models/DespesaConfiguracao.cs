using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DespesaConfiguracao")]
    public partial class DespesaConfiguracao
    {
        public DespesaConfiguracao()
        {
            DespesaPeriodos = new HashSet<DespesaPeriodo>();
        }

        [Key]
        public int CodDespesaConfiguracao { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PercentualKmCidade { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PercentualKmForaCidade { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValorRefeicaoLimiteTecnico { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValorRefeicaoLimiteOutros { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime HoraExtraInicioAlmoco { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime HoraExtraInicioJanta { get; set; }
        [Column("PercentualNotaKM", TypeName = "decimal(10, 2)")]
        public decimal PercentualNotaKm { get; set; }
        [Column("ValorKM", TypeName = "decimal(10, 2)")]
        public decimal ValorKm { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorAluguelCarro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataVigencia { get; set; }
        public byte IndAtivo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [InverseProperty(nameof(DespesaPeriodo.CodDespesaConfiguracaoNavigation))]
        public virtual ICollection<DespesaPeriodo> DespesaPeriodos { get; set; }
    }
}
