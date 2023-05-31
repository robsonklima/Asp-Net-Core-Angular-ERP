using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Performance_DistribuicaoChamadosCausaExtraMaquinaBBTecno2")]
    public partial class PerformanceDistribuicaoChamadosCausaExtraMaquinaBbtecno2
    {
        [Column("CodPerformance_DistribuicaoChamadosCausaExtraMaquina2")]
        public int CodPerformanceDistribuicaoChamadosCausaExtraMaquina2 { get; set; }
        public int? CodCliente { get; set; }
        public int? QtdEquipamentos { get; set; }
        public int? QtdChamados { get; set; }
        [StringLength(50)]
        public string Modelo { get; set; }
    }
}
