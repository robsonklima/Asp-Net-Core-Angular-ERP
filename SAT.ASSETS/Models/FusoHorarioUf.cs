using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("FusoHorarioUF")]
    public partial class FusoHorarioUf
    {
        [Key]
        [Column("CodFusoHorarioUF")]
        public int CodFusoHorarioUf { get; set; }
        [Column("CodUF")]
        public int CodUf { get; set; }
        public byte IndHorarioVerao { get; set; }
        public int HorarioUtc { get; set; }
    }
}
