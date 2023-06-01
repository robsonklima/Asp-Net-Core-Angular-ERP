using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcDashboardChamadosAntigo
    {
        public int? CodFilial { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [Required]
        [StringLength(50)]
        public string Cliente { get; set; }
        [StringLength(16)]
        public string Modelo { get; set; }
        [StringLength(50)]
        public string ModeloCompleto { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAbertura { get; set; }
        [StringLength(50)]
        public string Intervencao { get; set; }
        [Required]
        [Column("img")]
        [StringLength(50)]
        public string Img { get; set; }
    }
}
