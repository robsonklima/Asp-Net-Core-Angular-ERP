using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Intencao")]
    public partial class Intencao
    {
        [Key]
        public int CodIntencao { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        public int? CodTecnico { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        public byte? IndAtivo { get; set; }
    }
}
