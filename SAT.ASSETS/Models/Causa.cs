using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Causa")]
    public partial class Causa
    {
        [Key]
        public int CodTipoCausa { get; set; }
        [Key]
        public int CodGrupoCausa { get; set; }
        [Key]
        public int CodCausa { get; set; }
        [Column("CodECausa")]
        [StringLength(5)]
        public string CodEcausa { get; set; }
        [StringLength(70)]
        public string NomeCausa { get; set; }
        public byte? IndAtivo { get; set; }
        public int? CodTraducao { get; set; }

        [ForeignKey("CodTipoCausa,CodGrupoCausa")]
        [InverseProperty(nameof(GrupoCausa.Causas))]
        public virtual GrupoCausa Cod { get; set; }
    }
}
