using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwTarefasSlaGraficoSemanal
    {
        public int? Quantidade { get; set; }
        [Required]
        [StringLength(6)]
        public string Status { get; set; }
    }
}
