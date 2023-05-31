using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("LaudoControle")]
    public partial class LaudoControle
    {
        [Key]
        public int CodLaudoControle { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int? CodTecnico { get; set; }
        public int? IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}
