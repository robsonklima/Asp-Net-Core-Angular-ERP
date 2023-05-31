using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcParqueO
    {
        public int? CodEquipContrato { get; set; }
        public int? CodCliente { get; set; }
        public int? CodContrato { get; set; }
        public int? CodRegiao { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodFilial { get; set; }
        public int? CodTipoEquip { get; set; }
        public int? CodGrupoEquip { get; set; }
        public int? CodEquip { get; set; }
        public int? AnoMes { get; set; }
        public int? QtdDiaInstalado { get; set; }
        [Column("QtdOSGeral")]
        public int? QtdOsgeral { get; set; }
        [Column("QtdOSMaquina")]
        public int? QtdOsmaquina { get; set; }
        [Column("QtdOSExtraMaquina")]
        public int? QtdOsextraMaquina { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorReceita { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorDespesa { get; set; }
        public byte? IndEquipAtivo { get; set; }
        public byte? IndEquipReceita { get; set; }
        public byte? IndEquipGarantia { get; set; }
    }
}
