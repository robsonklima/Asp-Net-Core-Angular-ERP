using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("InstalAnexo")]
    public partial class InstalAnexo
    {
        [Key]
        public int CodInstalAnexo { get; set; }
        public int? CodInstalacao { get; set; }
        public int? CodInstalPleito { get; set; }
        public int? CodInstalLote { get; set; }
        [Required]
        [StringLength(200)]
        public string NomeAnexo { get; set; }
        [Required]
        [StringLength(200)]
        public string DescAnexo { get; set; }
        [Required]
        [StringLength(300)]
        public string SourceAnexo { get; set; }
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

        [ForeignKey(nameof(CodInstalLote))]
        [InverseProperty(nameof(InstalLote.InstalAnexos))]
        public virtual InstalLote CodInstalLoteNavigation { get; set; }
        [ForeignKey(nameof(CodInstalPleito))]
        [InverseProperty(nameof(InstalPleito.InstalAnexos))]
        public virtual InstalPleito CodInstalPleitoNavigation { get; set; }
        [ForeignKey(nameof(CodInstalacao))]
        [InverseProperty(nameof(Instalacao.InstalAnexos))]
        public virtual Instalacao CodInstalacaoNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.InstalAnexoCodUsuarioCadNavigations))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioManut))]
        [InverseProperty(nameof(Usuario.InstalAnexoCodUsuarioManutNavigations))]
        public virtual Usuario CodUsuarioManutNavigation { get; set; }
    }
}
