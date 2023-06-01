using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OSBancadaPecasAplic")]
    public partial class OsbancadaPecasAplic
    {
        [Key]
        [Column("CodOSBancada")]
        public int CodOsbancada { get; set; }
        [Key]
        [Column("CodPecaRE5114")]
        public int CodPecaRe5114 { get; set; }
        [Key]
        public int CodPeca { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? QtdAplicada { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }

        [ForeignKey("CodOsbancada,CodPecaRe5114")]
        [InverseProperty(nameof(OsbancadaPeca.OsbancadaPecasAplics))]
        public virtual OsbancadaPeca Cod { get; set; }
        [ForeignKey(nameof(CodPeca))]
        [InverseProperty(nameof(Peca.OsbancadaPecasAplics))]
        public virtual Peca CodPecaNavigation { get; set; }
    }
}
