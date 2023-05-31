using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class InstalRefOb
    {
        public InstalRefOb()
        {
            InstalObs = new HashSet<InstalOb>();
        }

        [Key]
        public int CodInstalRefObs { get; set; }
        [Required]
        [StringLength(20)]
        public string NomeRefObs { get; set; }
        public byte IndAtivo { get; set; }

        [InverseProperty(nameof(InstalOb.CodInstalRefObsNavigation))]
        public virtual ICollection<InstalOb> InstalObs { get; set; }
    }
}
