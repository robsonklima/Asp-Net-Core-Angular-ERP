using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcMtbf2
    {
        public int CodEquipContrato { get; set; }
        public int CodCliente { get; set; }
        public int CodContrato { get; set; }
        public int CodTipoEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodEquip { get; set; }
        public int CodFilial { get; set; }
        public int CodAutorizada { get; set; }
        public int CodRegiao { get; set; }
        public int? QtdDiaInstalado { get; set; }
        [Column("QtdMTBF")]
        public int? QtdMtbf { get; set; }
        [Column("QtdOS")]
        public int? QtdOs { get; set; }
        [Column("QtdOSGeral")]
        public int? QtdOsgeral { get; set; }
        [Column("PercentualMTBF", TypeName = "decimal(25, 13)")]
        public decimal? PercentualMtbf { get; set; }
    }
}
