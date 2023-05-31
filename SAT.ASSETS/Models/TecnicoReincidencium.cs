using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class TecnicoReincidencium
    {
        [Key]
        public int CodTecnicoReincidencia { get; set; }
        public int? Ordem { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(150)]
        public string NomeTecnico { get; set; }
        [StringLength(10)]
        public string Filial { get; set; }
        public int? QtdChamadosAtendidos { get; set; }
        public int? QtdChamadosReincidentes { get; set; }
        public double? Percentual { get; set; }
    }
}
