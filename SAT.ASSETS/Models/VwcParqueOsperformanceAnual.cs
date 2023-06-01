using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcParqueOsperformanceAnual
    {
        public int? CodEquipContrato { get; set; }
        public int? CodFilial { get; set; }
        public int? AnoMes { get; set; }
        public byte? IndAtivo { get; set; }
        public int? CodTipoEquip { get; set; }
        [Column("QtdOSGeral")]
        public int? QtdOsgeral { get; set; }
    }
}
