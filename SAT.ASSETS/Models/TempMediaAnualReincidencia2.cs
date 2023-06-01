using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("TempMediaAnualReincidencia2")]
    public partial class TempMediaAnualReincidencia2
    {
        public int? CodTipoEquip { get; set; }
        public int? CodFilial { get; set; }
        public int? ChamadosAno { get; set; }
        public int? ChamadosAnoReinc { get; set; }
        public int? ChamadosAnoReincMaquina { get; set; }
    }
}
