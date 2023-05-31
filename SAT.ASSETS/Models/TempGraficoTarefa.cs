using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class TempGraficoTarefa
    {
        [StringLength(10)]
        public string DataHora { get; set; }
        public int? QtdCadastro { get; set; }
        public int? TotalQtdCadastro { get; set; }
        public int? QtdEncerramento { get; set; }
        public int? TotalQtdEncerramento { get; set; }
        public int? QtdPendente { get; set; }
    }
}
