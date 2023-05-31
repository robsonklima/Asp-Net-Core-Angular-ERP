using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("GrupoCausa")]
    public partial class GrupoCausa
    {
        public GrupoCausa()
        {
            Causas = new HashSet<Causa>();
        }

        [Key]
        public int CodTipoCausa { get; set; }
        [Key]
        public int CodGrupoCausa { get; set; }
        [Column("CodEGrupoCausa")]
        [StringLength(3)]
        public string CodEgrupoCausa { get; set; }
        [StringLength(50)]
        public string NomeGrupoCausa { get; set; }
        public byte? IndAtivo { get; set; }
        public int? CodTraducao { get; set; }

        [ForeignKey(nameof(CodTipoCausa))]
        [InverseProperty(nameof(TipoCausa.GrupoCausas))]
        public virtual TipoCausa CodTipoCausaNavigation { get; set; }
        [InverseProperty(nameof(Causa.Cod))]
        public virtual ICollection<Causa> Causas { get; set; }
    }
}
