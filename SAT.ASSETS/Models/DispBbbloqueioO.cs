using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DispBBBloqueioOS")]
    public partial class DispBbbloqueioO
    {
        [Column("CodDispBBBloqueioOS")]
        public int CodDispBbbloqueioOs { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(50)]
        public string TipoBloqueio { get; set; }
    }
}
