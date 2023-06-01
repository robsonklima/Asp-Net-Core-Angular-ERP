using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class BloqueioReincidencium
    {
        public int CodBloqueioReincidencia { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("CodRATAtual")]
        public int? CodRatatual { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        public int IndBloqueioReincidencia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}
