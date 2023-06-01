using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("FaturamentoContratoControle")]
    public partial class FaturamentoContratoControle
    {
        [Key]
        public int CodFaturamento { get; set; }
        [Required]
        [StringLength(6)]
        public string AnoMes { get; set; }
        public int CodCliente { get; set; }
        public int CodContrato { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorReceita { get; set; }
        public int? CodStatusFaturamento { get; set; }
        [Column("proRata", TypeName = "decimal(10, 2)")]
        public decimal? ProRata { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorDesconto { get; set; }
        [StringLength(500)]
        public string Observacao { get; set; }
        [Column("NumNF")]
        [StringLength(30)]
        public string NumNf { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataEmissao { get; set; }
        public int? IndFaturado { get; set; }
        [StringLength(500)]
        public string CaminhoDanfe { get; set; }
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [StringLength(50)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public int? IndFaturamentoAutomatizado { get; set; }
        [StringLength(80)]
        public string Telefone { get; set; }
        [StringLength(1000)]
        public string Contato { get; set; }
        [Column("CNPJ")]
        [StringLength(50)]
        public string Cnpj { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorLiquido { get; set; }
        [Column("PIS", TypeName = "decimal(10, 2)")]
        public decimal? Pis { get; set; }
        [Column("COFINS", TypeName = "decimal(10, 2)")]
        public decimal? Cofins { get; set; }
        [Column("CSLL", TypeName = "decimal(10, 2)")]
        public decimal? Csll { get; set; }
        [Column("ISS", TypeName = "decimal(10, 2)")]
        public decimal? Iss { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorDuplicata { get; set; }
        [StringLength(30)]
        public string NomeFilial { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataVencimento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraLimiteFaturamento { get; set; }
    }
}
