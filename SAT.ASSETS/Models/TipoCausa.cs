using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoCausa")]
    public partial class TipoCausa
    {
        public TipoCausa()
        {
            GrupoCausas = new HashSet<GrupoCausa>();
        }

        [Key]
        public int CodTipoCausa { get; set; }
        [Column("CodETipoCausa")]
        [StringLength(2)]
        public string CodEtipoCausa { get; set; }
        [StringLength(50)]
        public string NomeTipoCausa { get; set; }
        public byte? IndAtivo { get; set; }
        public int? CodTraducao { get; set; }

        [InverseProperty(nameof(GrupoCausa.CodTipoCausaNavigation))]
        public virtual ICollection<GrupoCausa> GrupoCausas { get; set; }
    }
}
