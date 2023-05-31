using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("IntencaoCheckin")]
    public partial class IntencaoCheckin
    {
        [Key]
        public int CodIntencaoCheckin { get; set; }
        public int CodIntencao { get; set; }
        public int CodCheckin { get; set; }
        public double Distancia { get; set; }
        public double Tempo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}
