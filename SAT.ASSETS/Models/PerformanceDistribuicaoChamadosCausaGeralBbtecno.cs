using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Performance_DistribuicaoChamadosCausaGeralBBTecno")]
    public partial class PerformanceDistribuicaoChamadosCausaGeralBbtecno
    {
        [Column("CodPerformance_DistribuicaoChamadosCausaMaquina")]
        public int CodPerformanceDistribuicaoChamadosCausaMaquina { get; set; }
        public int? CodCliente { get; set; }
        public int? QtdChamados { get; set; }
        public int? QtdEquipamentos { get; set; }
        [StringLength(50)]
        public string Modelo { get; set; }
    }
}
