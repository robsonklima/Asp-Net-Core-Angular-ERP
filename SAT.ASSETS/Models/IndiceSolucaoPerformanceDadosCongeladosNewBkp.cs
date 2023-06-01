using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("IndiceSolucaoPerformanceDadosCongeladosNewBKP")]
    public partial class IndiceSolucaoPerformanceDadosCongeladosNewBkp
    {
        public int? CodTipoEquip { get; set; }
        public int? CodCliente { get; set; }
        public int? CodFilial { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodRegiao { get; set; }
        [Column("CodUF")]
        public int? CodUf { get; set; }
        public int? ChamadosMes { get; set; }
        [Column("ChamadosMesStatusSLA")]
        public int? ChamadosMesStatusSla { get; set; }
        public int? ChamadosMesAbertos { get; set; }
        [Column("ChamadosMesStatusSLAAbertos")]
        public int? ChamadosMesStatusSlaabertos { get; set; }
        [StringLength(6)]
        public string AnoMes { get; set; }
        [StringLength(4)]
        public string Ano { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Data { get; set; }
    }
}
