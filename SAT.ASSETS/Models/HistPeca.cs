using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("HistPeca")]
    public partial class HistPeca
    {
        [Key]
        public int CodHistorico { get; set; }
        [StringLength(20)]
        public string CodUsuarioHist { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraHist { get; set; }
        public int CodPeca { get; set; }
        [StringLength(20)]
        public string CodMagnus { get; set; }
        [StringLength(80)]
        public string NomePeca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValCusto { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValCustoDolar { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValPeca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValPecaDolar { get; set; }
        [Column("ValIPI", TypeName = "decimal(10, 2)")]
        public decimal? ValIpi { get; set; }
        public int? QtdMinimaVenda { get; set; }
        public int? CodPecaFamilia { get; set; }
        public int? CodPecaSubstituicao { get; set; }
        public int? CodPecaStatus { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public int? CodTraducao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraAtualizacaoValor { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAtualizacao { get; set; }
    }
}
