using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwTarefasSlaGraficoDiasRetorno
    {
        public string MesAno { get; set; }
        public string AnoMesAux { get; set; }
        public int? Horas { get; set; }
    }
}
