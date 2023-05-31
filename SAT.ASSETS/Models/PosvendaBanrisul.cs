using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("POSVendaBanrisul")]
    public partial class PosvendaBanrisul
    {
        [Key]
        [Column("CodPOSVendaBanrisul")]
        public int CodPosvendaBanrisul { get; set; }
        [Required]
        [StringLength(50)]
        public string NumeroNotaFiscal { get; set; }
        [Required]
        [StringLength(50)]
        public string SerieNotaFiscal { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataEmissao { get; set; }
        public int Garantia { get; set; }
        [Required]
        [StringLength(50)]
        public string Versao { get; set; }
        [Required]
        [StringLength(50)]
        public string NumeroSerie { get; set; }
        [Required]
        [StringLength(50)]
        public string Patrimonio { get; set; }
        public int SequenciaArquivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        public bool IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataDesativacao { get; set; }
        [StringLength(20)]
        public string CodUsuarioDesativacao { get; set; }

        [ForeignKey(nameof(CodUsuarioCadastro))]
        [InverseProperty(nameof(Usuario.PosvendaBanrisulCodUsuarioCadastroNavigations))]
        public virtual Usuario CodUsuarioCadastroNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioDesativacao))]
        [InverseProperty(nameof(Usuario.PosvendaBanrisulCodUsuarioDesativacaoNavigations))]
        public virtual Usuario CodUsuarioDesativacaoNavigation { get; set; }
    }
}
