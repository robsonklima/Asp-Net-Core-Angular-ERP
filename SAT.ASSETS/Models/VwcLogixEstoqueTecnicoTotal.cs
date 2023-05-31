using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcLogixEstoqueTecnicoTotal
    {
        [Column("CODTECNICO")]
        public int Codtecnico { get; set; }
        [Column("CODPECA")]
        public int? Codpeca { get; set; }
        [Column("CODFILIAL")]
        public int? Codfilial { get; set; }
        [Column("NOME")]
        [StringLength(50)]
        public string Nome { get; set; }
        [Column("DATAEMISSAOANTIGA")]
        [StringLength(50)]
        public string Dataemissaoantiga { get; set; }
        [Column("RETIRADA", TypeName = "decimal(38, 2)")]
        public decimal? Retirada { get; set; }
        [Column("DEVOLVIDA", TypeName = "decimal(38, 2)")]
        public decimal? Devolvida { get; set; }
        [Column("ESTOQUE", TypeName = "decimal(38, 2)")]
        public decimal? Estoque { get; set; }
    }
}
