using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcPrevisaoFaturamento
    {
        [StringLength(60)]
        public string AnoMes { get; set; }
        public int? CodCliente { get; set; }
        [StringLength(50)]
        public string RazaoSocial { get; set; }
        [Column("valor", TypeName = "money")]
        public decimal? Valor { get; set; }
        [Column("valorMAN", TypeName = "money")]
        public decimal? ValorMan { get; set; }
        [Column("valorEXGAR", TypeName = "money")]
        public decimal? ValorExgar { get; set; }
    }
}
