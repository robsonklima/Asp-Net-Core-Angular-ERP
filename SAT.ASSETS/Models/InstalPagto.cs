using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("InstalPagto")]
    public partial class InstalPagto
    {
        public InstalPagto()
        {
            InstalPagtoInstals = new HashSet<InstalPagtoInstal>();
        }

        [Key]
        public int CodInstalPagto { get; set; }
        public int CodContrato { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataPagto { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal VlrPagto { get; set; }
        [StringLength(100)]
        public string ObsPagto { get; set; }
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
        [InverseProperty(nameof(Contrato.InstalPagtos))]
        public virtual Contrato CodContratoNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.InstalPagtoCodUsuarioCadNavigations))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioManut))]
        [InverseProperty(nameof(Usuario.InstalPagtoCodUsuarioManutNavigations))]
        public virtual Usuario CodUsuarioManutNavigation { get; set; }
        [InverseProperty(nameof(InstalPagtoInstal.CodInstalPagtoNavigation))]
        public virtual ICollection<InstalPagtoInstal> InstalPagtoInstals { get; set; }
    }
}
