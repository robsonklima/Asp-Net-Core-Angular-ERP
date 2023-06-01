using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Turno")]
    public partial class Turno
    {
        [Key]
        public int CodTurno { get; set; }
        [Required]
        [StringLength(50)]
        public string DescTurno { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime HoraInicio1 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime HoraFim1 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime HoraInicio2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime HoraFim2 { get; set; }
        public byte IndAtivo { get; set; }
    }
}
