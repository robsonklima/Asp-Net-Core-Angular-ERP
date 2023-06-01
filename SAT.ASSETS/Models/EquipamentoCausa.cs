using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("EquipamentoCausa")]
    public partial class EquipamentoCausa
    {
        public int? CodEquip { get; set; }
        [Column("CodECausa")]
        [StringLength(5)]
        public string CodEcausa { get; set; }
    }
}
