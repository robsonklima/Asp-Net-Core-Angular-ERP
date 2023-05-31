using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("AnaliseDisponibilidadeBB")]
    public partial class AnaliseDisponibilidadeBb
    {
        [Column("ANOMES")]
        [StringLength(7)]
        public string Anomes { get; set; }
        [Column("NUMEROOS")]
        public string Numeroos { get; set; }
        [Column("NUMEROBEM")]
        public string Numerobem { get; set; }
        [Column("DENTRODOPRAZO")]
        [StringLength(2)]
        public string Dentrodoprazo { get; set; }
        [Column("CRITICIDADE")]
        [StringLength(10)]
        public string Criticidade { get; set; }
        [Column("DTACHAMADA", TypeName = "datetime")]
        public DateTime? Dtachamada { get; set; }
        [Column("DTAFECHAMENTO", TypeName = "datetime")]
        public DateTime? Dtafechamento { get; set; }
        [Column("MOTIVO")]
        [StringLength(3)]
        public string Motivo { get; set; }
        [Column("DISTANCIA")]
        public int? Distancia { get; set; }
    }
}
