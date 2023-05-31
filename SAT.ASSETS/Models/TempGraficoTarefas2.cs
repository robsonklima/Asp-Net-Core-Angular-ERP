using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("TempGraficoTarefas2")]
    public partial class TempGraficoTarefas2
    {
        [StringLength(10)]
        public string DataHora { get; set; }
        public int? QtdCadastro { get; set; }
        public int? QtdEncerramentoDia { get; set; }
        public int? QtdEncerramentoDiaAnterior { get; set; }
        public int? QtdEncerramento { get; set; }
        public int? QtdPendente { get; set; }
    }
}
