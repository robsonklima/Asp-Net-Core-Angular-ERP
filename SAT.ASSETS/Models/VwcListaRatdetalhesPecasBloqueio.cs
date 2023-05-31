using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcListaRatdetalhesPecasBloqueio
    {
        [StringLength(205)]
        public string Dados { get; set; }
        public int CodRatDetalhe { get; set; }
        [Column("CodRATDetalhesPecas")]
        public int CodRatdetalhesPecas { get; set; }
    }
}
