using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class InstalMotivoRe
    {
        public InstalMotivoRe()
        {
            InstalRessalvas = new HashSet<InstalRessalva>();
        }

        [Key]
        public int CodInstalMotivoRes { get; set; }
        [Required]
        [StringLength(50)]
        public string DescMotivoRes { get; set; }
        [Required]
        [StringLength(3)]
        public string SiglaMotivoRes { get; set; }
        public byte IndTipoRes { get; set; }
        public byte IndAtivo { get; set; }

        [InverseProperty(nameof(InstalRessalva.CodInstalMotivoResNavigation))]
        public virtual ICollection<InstalRessalva> InstalRessalvas { get; set; }
    }
}
