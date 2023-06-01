using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DispBBDissuasao")]
    public partial class DispBbdissuasao
    {
        [Column("CodDispBBDissuasao")]
        public int CodDispBbdissuasao { get; set; }
        public int CodEquipContrato { get; set; }
        public int IndDissuasao { get; set; }
    }
}
