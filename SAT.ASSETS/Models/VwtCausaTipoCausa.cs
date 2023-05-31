using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwtCausaTipoCausa
    {
        public int CodCausa { get; set; }
        [Column("CodECausa")]
        [StringLength(5)]
        public string CodEcausa { get; set; }
        public int CodGrupoCausa { get; set; }
        [StringLength(50)]
        public string NomeCausa { get; set; }
        public int? CodTipoCausa { get; set; }
        [Column("CodETipoCausa")]
        [StringLength(2)]
        public string CodEtipoCausa { get; set; }
        [Required]
        [StringLength(5)]
        public string Culture { get; set; }
    }
}
