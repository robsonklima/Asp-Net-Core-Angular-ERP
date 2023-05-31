using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcLogixEstoqueTecnico
    {
        public int CodTecnico { get; set; }
        public int CodPeca { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal? Retirada { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal? Devolvida { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal? Estoque { get; set; }
        public int? CodFilial { get; set; }
        [StringLength(50)]
        public string Nome { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Expr1 { get; set; }
        [Column("dataEmissaoAntiga", TypeName = "datetime")]
        public DateTime? DataEmissaoAntiga { get; set; }
    }
}
