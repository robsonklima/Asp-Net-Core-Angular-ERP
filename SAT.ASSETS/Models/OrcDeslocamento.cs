using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcDeslocamento")]
    public partial class OrcDeslocamento
    {
        [Key]
        public int CodOrcDeslocamento { get; set; }
        public int? CodOrc { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? QuantidadeHoraCadaSessentaKm { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorUnitarioKmRodado { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? QuantidadeKm { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorTotalKmRodado { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorTotalKmDeslocamento { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorHoraDeslocamento { get; set; }
        [Column(TypeName = "decimal(18, 10)")]
        public decimal? LatitudeOrigem { get; set; }
        [Column(TypeName = "decimal(18, 10)")]
        public decimal? LongitudeOrigem { get; set; }
        [Column(TypeName = "decimal(18, 10)")]
        public decimal? LatitudeDestino { get; set; }
        [Column(TypeName = "decimal(18, 10)")]
        public decimal? LongitudeDestino { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(50)]
        public string UsuarioCadastro { get; set; }

        [ForeignKey(nameof(CodOrc))]
        [InverseProperty(nameof(Orc.OrcDeslocamentos))]
        public virtual Orc CodOrcNavigation { get; set; }
    }
}
