using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DispBBEquipCriticidade")]
    public partial class DispBbequipCriticidade
    {
        public int? CodEquip { get; set; }
        [Column("CodDispBBCriticidade")]
        public int? CodDispBbcriticidade { get; set; }
        public double? Valor { get; set; }
    }
}
