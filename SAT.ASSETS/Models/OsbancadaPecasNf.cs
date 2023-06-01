using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OSBancadaPecasNF")]
    public partial class OsbancadaPecasNf
    {
        [Key]
        [Column("CodOSBancada")]
        public int CodOsbancada { get; set; }
        [Key]
        [Column("CodPecaRE5114")]
        public int CodPecaRe5114 { get; set; }
        [Key]
        [Column("CodNF")]
        public int CodNf { get; set; }

        [ForeignKey(nameof(CodNf))]
        [InverseProperty(nameof(Nf.OsbancadaPecasNfs))]
        public virtual Nf CodNfNavigation { get; set; }
    }
}
