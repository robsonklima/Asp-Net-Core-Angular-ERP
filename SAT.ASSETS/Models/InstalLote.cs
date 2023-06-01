using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("InstalLote")]
    public partial class InstalLote
    {
        public InstalLote()
        {
            InstalAnexos = new HashSet<InstalAnexo>();
        }

        [Key]
        public int CodInstalLote { get; set; }
        [Required]
        [StringLength(100)]
        public string NomeLote { get; set; }
        [StringLength(500)]
        public string DescLote { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataRecLote { get; set; }
        public int CodContrato { get; set; }
        public int QtdEquipLote { get; set; }
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
        [InverseProperty(nameof(Contrato.InstalLotes))]
        public virtual Contrato CodContratoNavigation { get; set; }
        [InverseProperty(nameof(InstalAnexo.CodInstalLoteNavigation))]
        public virtual ICollection<InstalAnexo> InstalAnexos { get; set; }
    }
}
