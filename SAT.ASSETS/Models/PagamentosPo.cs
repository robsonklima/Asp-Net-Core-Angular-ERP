using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PagamentosPOS")]
    public partial class PagamentosPo
    {
        [Key]
        [Column("CodPagamentosPOS")]
        public int CodPagamentosPos { get; set; }
        [Column("IA", TypeName = "decimal(18, 2)")]
        public decimal? Ia { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Banrisul { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BanrisulVenda { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BanrisulTradeIn { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BanrisulManutencao { get; set; }
        [Column("CDSVero", TypeName = "decimal(18, 2)")]
        public decimal? Cdsvero { get; set; }
        [Column("CDSSicredi", TypeName = "decimal(18, 2)")]
        public decimal? Cdssicredi { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Solutech { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SolutechSicredi { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Data { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Sicredi { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SicrediManutencao { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SicrediCancelamento { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SicrediDesinstalacao { get; set; }
        [StringLength(50)]
        public string IntervaloSicredi { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BanrisulManutencaoVigente { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BanrisulDataVigente { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BanrisulManutencaoTradeInVigente { get; set; }

        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.PagamentosPos))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
