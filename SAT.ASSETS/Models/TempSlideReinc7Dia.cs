using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class TempSlideReinc7Dia
    {
        [StringLength(50)]
        public string NomeTecnico { get; set; }
        [StringLength(3)]
        public string NomeFilial { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Data { get; set; }
        public int? QtdChamadosAtendidos { get; set; }
        public int? QtdChamadosReincidentes { get; set; }
    }
}
