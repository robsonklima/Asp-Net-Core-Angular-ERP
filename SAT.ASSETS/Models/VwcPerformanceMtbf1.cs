using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcPerformanceMtbf1
    {
        public int CodEquipContrato { get; set; }
        public int? QtdDiaInstalado { get; set; }
        [Column("QtdOSGeral")]
        public int? QtdOsgeral { get; set; }
        [Column("MTBF")]
        public int? Mtbf { get; set; }
    }
}
