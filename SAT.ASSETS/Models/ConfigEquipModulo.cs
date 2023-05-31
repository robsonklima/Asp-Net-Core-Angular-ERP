using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class ConfigEquipModulo
    {
        public int? CodEquip { get; set; }
        [Column("CodECausa")]
        [StringLength(20)]
        public string CodEcausa { get; set; }
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [StringLength(50)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [Column("indAtivo")]
        public int? IndAtivo { get; set; }
    }
}
