using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("HorarioVerao")]
    public partial class HorarioVerao
    {
        [Key]
        public int CodHorarioVerao { get; set; }
        public int Ano { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHorarioVeraoIni { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHorarioVeraoFim { get; set; }
    }
}
