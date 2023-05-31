using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("IndicePecasPendDet")]
    public partial class IndicePecasPendDet
    {
        public int CodIndPecasPend { get; set; }
        public int? ChamadosMes { get; set; }
        public int? ChamadosMesPecasPendentes { get; set; }
        [StringLength(6)]
        public string AnoMes { get; set; }
    }
}
