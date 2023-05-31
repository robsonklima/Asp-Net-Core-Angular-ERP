using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("InstalPleitoInstal")]
    public partial class InstalPleitoInstal
    {
        [Key]
        public int CodInstalacao { get; set; }
        [Key]
        public int CodInstalPleito { get; set; }
        public int? CodEquipContrato { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [ForeignKey(nameof(CodInstalPleito))]
        [InverseProperty(nameof(InstalPleito.InstalPleitoInstals))]
        public virtual InstalPleito CodInstalPleitoNavigation { get; set; }
        [ForeignKey(nameof(CodInstalacao))]
        [InverseProperty(nameof(Instalacao.InstalPleitoInstals))]
        public virtual Instalacao CodInstalacaoNavigation { get; set; }
    }
}
