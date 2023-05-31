using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcProdutividadeTecnicosMesAtual
    {
        [StringLength(50)]
        public string Tecnico { get; set; }
        [Required]
        [StringLength(50)]
        public string Filial { get; set; }
        public int? Atend { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? MediaDistancia { get; set; }
        [StringLength(7)]
        public string MediaTempo { get; set; }
        public int? AtendCapital { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal MediaDistanciaCap { get; set; }
        [Required]
        [StringLength(7)]
        public string MediaTempoCap { get; set; }
        public int? AtendInterior { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal MediaDistanciaInt { get; set; }
        [Required]
        [StringLength(7)]
        public string MediaTempoInt { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? MediaAtendDiario { get; set; }
        public int? AtendTempoMedio { get; set; }
    }
}
