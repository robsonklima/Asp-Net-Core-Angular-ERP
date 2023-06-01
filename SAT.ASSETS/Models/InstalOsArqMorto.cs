using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("InstalOS_ARQ_MORTO")]
    public partial class InstalOsArqMorto
    {
        public int CodInstalacao { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("CodRAT")]
        public int? CodRat { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}
