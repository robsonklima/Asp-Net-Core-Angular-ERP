using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RATDetalhesPecas")]
    public partial class RatdetalhesPeca
    {
        [Key]
        [Column("CodRATDetalhesPecas")]
        public int CodRatdetalhesPecas { get; set; }
        public int CodRatDetalhe { get; set; }
        public int CodPeca { get; set; }
        public int QtdePecas { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValPecas { get; set; }
        [Column("A_P")]
        public int? AP { get; set; }
        [Column("DatIncPP", TypeName = "datetime")]
        public DateTime? DatIncPp { get; set; }
        public int? QtdeLib { get; set; }
        [StringLength(100)]
        public string DescStatus { get; set; }
        public int? CodPecaSubst { get; set; }
        public byte? IndPecaSubst { get; set; }
        public byte? IndCentral { get; set; }
        [Column("IndOK")]
        public byte? IndOk { get; set; }
        public byte? IndNotaFiscal { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [StringLength(20)]
        public string CodUsuarioManutencao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataManutencao { get; set; }
        [StringLength(20)]
        public string CodMagnusInconsistente { get; set; }
        public byte? IndPassivelConserto { get; set; }
        [StringLength(50)]
        public string NotaFiscal { get; set; }
        public int? NfStatus { get; set; }
        [StringLength(50)]
        public string NumSerie { get; set; }
        [StringLength(500)]
        public string MotivoSubstituicao { get; set; }

        [ForeignKey(nameof(CodPeca))]
        [InverseProperty(nameof(Peca.RatdetalhesPecas))]
        public virtual Peca CodPecaNavigation { get; set; }
        [ForeignKey(nameof(CodRatDetalhe))]
        [InverseProperty(nameof(Ratdetalhe.RatdetalhesPecas))]
        public virtual Ratdetalhe CodRatDetalheNavigation { get; set; }
    }
}
