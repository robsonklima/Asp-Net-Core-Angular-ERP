using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class PontoUsuarioDataModoDivergencium
    {
        public PontoUsuarioDataModoDivergencium()
        {
            PontoUsuarioDataDivergencia = new HashSet<PontoUsuarioDataDivergencium>();
        }

        [Key]
        public int CodPontoUsuarioDataModoDivergencia { get; set; }
        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [InverseProperty(nameof(PontoUsuarioDataDivergencium.CodPontoUsuarioDataModoDivergenciaNavigation))]
        public virtual ICollection<PontoUsuarioDataDivergencium> PontoUsuarioDataDivergencia { get; set; }
    }
}
