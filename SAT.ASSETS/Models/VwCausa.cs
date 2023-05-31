using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwCausa
    {
        public int CodCausa { get; set; }
        public int CodTipoCausa { get; set; }
        public int CodGrupoCausa { get; set; }
        [Column("CodEGrupoCausa")]
        [StringLength(3)]
        public string CodEgrupoCausa { get; set; }
        [StringLength(50)]
        public string NomeGrupoCausa { get; set; }
        [Column("CodETipoCausa")]
        [StringLength(2)]
        public string CodEtipoCausa { get; set; }
        [StringLength(50)]
        public string NomeTipoCausa { get; set; }
        [Column("CodECausa")]
        [StringLength(5)]
        public string CodEcausa { get; set; }
        [StringLength(70)]
        public string NomeCausa { get; set; }
    }
}
