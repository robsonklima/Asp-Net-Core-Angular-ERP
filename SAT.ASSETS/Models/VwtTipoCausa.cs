using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwtTipoCausa
    {
        public int CodTipoCausa { get; set; }
        [Column("CodETipoCausa")]
        [StringLength(2)]
        public string CodEtipoCausa { get; set; }
        [StringLength(50)]
        public string NomeTipoCausa { get; set; }
        public byte? IndAtivo { get; set; }
        [Required]
        [StringLength(5)]
        public string Culture { get; set; }
    }
}
