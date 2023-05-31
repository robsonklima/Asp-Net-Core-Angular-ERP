using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("TecnicoProdutividade")]
    public partial class TecnicoProdutividade
    {
        public int? CodTecnico { get; set; }
        public int? Atend { get; set; }
        public double? MediaDistancia { get; set; }
        public double? MediaTempo { get; set; }
        public int? AtendCapital { get; set; }
        public double? MediaDistanciaCap { get; set; }
        public double? MediaTempoCap { get; set; }
        public int? AtendInterior { get; set; }
        public double? MediaDistanciaInt { get; set; }
        public double? MediaTempoInt { get; set; }
        public double? MediaAtendDiario { get; set; }
        public double? AtendTempoMedio { get; set; }
        [StringLength(50)]
        public string Controle { get; set; }
        public double? Eficiencia { get; set; }
        public double? TempoMedioJornadaMinutos { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
    }
}
