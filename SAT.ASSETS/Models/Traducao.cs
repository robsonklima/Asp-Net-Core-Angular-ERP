using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Traducao")]
    public partial class Traducao
    {
        public int CodTraducao { get; set; }
        public int CodLingua { get; set; }
        [Column("CodETraducao")]
        [StringLength(20)]
        public string CodEtraducao { get; set; }
        [StringLength(50)]
        public string NomeTraducao { get; set; }
        [StringLength(100)]
        public string DescTraducao { get; set; }

        [ForeignKey(nameof(CodLingua))]
        public virtual Lingua CodLinguaNavigation { get; set; }
    }
}
