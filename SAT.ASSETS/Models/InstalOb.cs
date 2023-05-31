using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class InstalOb
    {
        [Key]
        public int CodInstalObs { get; set; }
        public int CodInstalacao { get; set; }
        [Required]
        [StringLength(8000)]
        public string Observacao { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        public int CodInstalRefObs { get; set; }
        public byte IndJustificativa { get; set; }
        public byte IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodInstalRefObs))]
        [InverseProperty(nameof(InstalRefOb.InstalObs))]
        public virtual InstalRefOb CodInstalRefObsNavigation { get; set; }
        [ForeignKey(nameof(CodInstalacao))]
        [InverseProperty(nameof(Instalacao.InstalObs))]
        public virtual Instalacao CodInstalacaoNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.InstalObCodUsuarioCadNavigations))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioManut))]
        [InverseProperty(nameof(Usuario.InstalObCodUsuarioManutNavigations))]
        public virtual Usuario CodUsuarioManutNavigation { get; set; }
    }
}
