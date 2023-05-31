using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DispBBCalcOSExcluidos")]
    public partial class DispBbcalcOsexcluido
    {
        [Column("CodDispBBCalcOSExcluidos")]
        public int CodDispBbcalcOsexcluidos { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
    }
}
