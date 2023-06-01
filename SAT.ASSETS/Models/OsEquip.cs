using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OS_Equip")]
    public partial class OsEquip
    {
        [Key]
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int CodEquip { get; set; }
    }
}
