using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcListaRatdetalhesBloqueio
    {
        [Required]
        [StringLength(431)]
        public string Dados { get; set; }
        [Column("CodRATDetalhe")]
        public int CodRatdetalhe { get; set; }
        [Column("CodRAT")]
        public int CodRat { get; set; }
    }
}
