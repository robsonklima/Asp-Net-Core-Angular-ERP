using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DashboardReincidenciaPorEquipamento")]
    public partial class DashboardReincidenciaPorEquipamento
    {
        [StringLength(4)]
        public string Ano { get; set; }
        [StringLength(4)]
        public string Mes { get; set; }
        [StringLength(6)]
        public string AnoMes { get; set; }
        public int? CodEquip { get; set; }
        public int? CodCliente { get; set; }
        public int? CodFilial { get; set; }
        public int? ChamadosMes { get; set; }
        public int? ChamadosMesReinc { get; set; }
        [StringLength(100)]
        public string NumSerie { get; set; }
    }
}
