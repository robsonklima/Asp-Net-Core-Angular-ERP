using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("InstalTipoPleito")]
    public partial class InstalTipoPleito
    {
        public InstalTipoPleito()
        {
            InstalPleitos = new HashSet<InstalPleito>();
        }

        [Key]
        public int CodInstalTipoPleito { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeTipoPleito { get; set; }
        [StringLength(100)]
        public string DescTipoPleito { get; set; }
        public byte IndAtivo { get; set; }
        [StringLength(800)]
        public string IntroTipoPleito { get; set; }

        [InverseProperty(nameof(InstalPleito.CodInstalTipoPleitoNavigation))]
        public virtual ICollection<InstalPleito> InstalPleitos { get; set; }
    }
}
