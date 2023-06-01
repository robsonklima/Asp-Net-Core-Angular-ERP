using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class InstalMotivoMultum
    {
        public InstalMotivoMultum()
        {
            InstalPagtoInstals = new HashSet<InstalPagtoInstal>();
        }

        [Key]
        public int CodInstalMotivoMulta { get; set; }
        [Required]
        [StringLength(20)]
        public string NomeMotivoMulta { get; set; }
        [StringLength(50)]
        public string DescMotivoMulta { get; set; }
        public byte IndAtivo { get; set; }

        [InverseProperty(nameof(InstalPagtoInstal.CodInstalMotivoMultaNavigation))]
        public virtual ICollection<InstalPagtoInstal> InstalPagtoInstals { get; set; }
    }
}
