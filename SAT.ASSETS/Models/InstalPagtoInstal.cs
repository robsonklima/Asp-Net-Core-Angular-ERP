using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("InstalPagtoInstal")]
    public partial class InstalPagtoInstal
    {
        [Key]
        public int CodInstalacao { get; set; }
        [Key]
        public int CodInstalPagto { get; set; }
        [Key]
        public int CodInstalTipoParcela { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal VlrParcela { get; set; }
        public int? CodInstalMotivoMulta { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? VlrMulta { get; set; }
        public byte IndEndossarMulta { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [StringLength(200)]
        public string Comentario { get; set; }
        public byte? IndImportacao { get; set; }

        [ForeignKey(nameof(CodInstalMotivoMulta))]
        [InverseProperty(nameof(InstalMotivoMultum.InstalPagtoInstals))]
        public virtual InstalMotivoMultum CodInstalMotivoMultaNavigation { get; set; }
        [ForeignKey(nameof(CodInstalPagto))]
        [InverseProperty(nameof(InstalPagto.InstalPagtoInstals))]
        public virtual InstalPagto CodInstalPagtoNavigation { get; set; }
        [ForeignKey(nameof(CodInstalTipoParcela))]
        [InverseProperty(nameof(InstalTipoParcela.InstalPagtoInstals))]
        public virtual InstalTipoParcela CodInstalTipoParcelaNavigation { get; set; }
        [ForeignKey(nameof(CodInstalacao))]
        [InverseProperty(nameof(Instalacao.InstalPagtoInstals))]
        public virtual Instalacao CodInstalacaoNavigation { get; set; }
    }
}
