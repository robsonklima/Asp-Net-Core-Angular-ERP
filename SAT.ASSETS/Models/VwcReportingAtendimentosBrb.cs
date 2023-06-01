using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReportingAtendimentosBrb
    {
        [Required]
        [StringLength(10)]
        public string MesAtendimento { get; set; }
        public int? InstaladosAcumulado { get; set; }
        public int? InstaladosMes { get; set; }
        public int? ChamadosCausaMaquina { get; set; }
        public int? ChamadosExtraMaquina { get; set; }
        public int? ChamadosAlteracaoDeEngenharia { get; set; }
        public int? DemaisChamados { get; set; }
        [Column("TOTALCHAMADOS")]
        public int? Totalchamados { get; set; }
    }
}
