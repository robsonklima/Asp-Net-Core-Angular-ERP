using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("InstalTipoParcela")]
    public partial class InstalTipoParcela
    {
        public InstalTipoParcela()
        {
            InstalPagtoInstals = new HashSet<InstalPagtoInstal>();
        }

        [Key]
        public int CodInstalTipoParcela { get; set; }
        [Required]
        [StringLength(30)]
        public string NomeTipoParcela { get; set; }
        public byte IndAtivo { get; set; }

        [InverseProperty(nameof(InstalPagtoInstal.CodInstalTipoParcelaNavigation))]
        public virtual ICollection<InstalPagtoInstal> InstalPagtoInstals { get; set; }
    }
}
