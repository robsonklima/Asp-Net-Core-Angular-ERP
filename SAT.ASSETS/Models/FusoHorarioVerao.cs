using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("FusoHorarioVerao")]
    public partial class FusoHorarioVerao
    {
        [Key]
        public int CodFuso { get; set; }
        public int Fuso { get; set; }
        [Column("H_Verao")]
        public int HVerao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime InicioPeriodo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FimPeriodo { get; set; }
    }
}
