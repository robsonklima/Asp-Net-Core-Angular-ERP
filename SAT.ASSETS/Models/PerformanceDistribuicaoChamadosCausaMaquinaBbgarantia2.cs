using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Performance_DistribuicaoChamadosCausaMaquinaBBGarantia2")]
    public partial class PerformanceDistribuicaoChamadosCausaMaquinaBbgarantia2
    {
        [Column("CodPerformance_DistribuicaoChamadosCausaMaquina2")]
        public int CodPerformanceDistribuicaoChamadosCausaMaquina2 { get; set; }
        public int? CodCliente { get; set; }
        public int? QtdEquipamentos { get; set; }
        public int? QtdChamados { get; set; }
        [StringLength(50)]
        public string Modelo { get; set; }
    }
}
