using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Tmp_Calcula_Parque_Meses")]
    public partial class TmpCalculaParqueMese
    {
        [StringLength(20)]
        public string Pai { get; set; }
        public int? AnoMes { get; set; }
    }
}
