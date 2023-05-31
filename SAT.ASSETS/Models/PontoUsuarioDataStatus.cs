using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PontoUsuarioDataStatus")]
    public partial class PontoUsuarioDataStatus
    {
        public PontoUsuarioDataStatus()
        {
            PontoUsuarioData = new HashSet<PontoUsuarioDatum>();
        }

        [Key]
        public int CodPontoUsuarioDataStatus { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }

        [InverseProperty(nameof(PontoUsuarioDatum.CodPontoUsuarioDataStatusNavigation))]
        public virtual ICollection<PontoUsuarioDatum> PontoUsuarioData { get; set; }
    }
}
