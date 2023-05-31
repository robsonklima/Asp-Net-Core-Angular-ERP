using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("InstalPleito")]
    public partial class InstalPleito
    {
        public InstalPleito()
        {
            InstalAnexos = new HashSet<InstalAnexo>();
            InstalPleitoInstals = new HashSet<InstalPleitoInstal>();
            InstalPleitoObs = new HashSet<InstalPleitoOb>();
        }

        [Key]
        public int CodInstalPleito { get; set; }
        public int CodContrato { get; set; }
        public int CodInstalTipoPleito { get; set; }
        [Required]
        [StringLength(50)]
        public string NomePleito { get; set; }
        [StringLength(200)]
        public string DescPleito { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataEnvio { get; set; }
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

        [ForeignKey(nameof(CodContrato))]
        [InverseProperty(nameof(Contrato.InstalPleitos))]
        public virtual Contrato CodContratoNavigation { get; set; }
        [ForeignKey(nameof(CodInstalTipoPleito))]
        [InverseProperty(nameof(InstalTipoPleito.InstalPleitos))]
        public virtual InstalTipoPleito CodInstalTipoPleitoNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.InstalPleitoCodUsuarioCadNavigations))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioManut))]
        [InverseProperty(nameof(Usuario.InstalPleitoCodUsuarioManutNavigations))]
        public virtual Usuario CodUsuarioManutNavigation { get; set; }
        [InverseProperty(nameof(InstalAnexo.CodInstalPleitoNavigation))]
        public virtual ICollection<InstalAnexo> InstalAnexos { get; set; }
        [InverseProperty(nameof(InstalPleitoInstal.CodInstalPleitoNavigation))]
        public virtual ICollection<InstalPleitoInstal> InstalPleitoInstals { get; set; }
        [InverseProperty(nameof(InstalPleitoOb.CodInstalPleitoNavigation))]
        public virtual ICollection<InstalPleitoOb> InstalPleitoObs { get; set; }
    }
}
