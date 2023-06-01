using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("LocaisSLABanrisul")]
    public partial class LocaisSlabanrisul
    {
        [Column("codLocaisBanrisul")]
        public int CodLocaisBanrisul { get; set; }
        public int? CodPosto { get; set; }
        [StringLength(5)]
        public string Inicio { get; set; }
        [StringLength(5)]
        public string Fim { get; set; }
    }
}
