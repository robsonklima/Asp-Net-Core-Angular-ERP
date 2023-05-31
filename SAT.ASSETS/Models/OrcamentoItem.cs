using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcamentoItem")]
    public partial class OrcamentoItem
    {
        public int? CodOrcamento { get; set; }
        [Key]
        public int CodItemOrcamento { get; set; }
        public int? CodServico { get; set; }
        public int? CodPeca { get; set; }
        [StringLength(100)]
        public string DescItem { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValUnitarioItem { get; set; }
        public int? QtdeItem { get; set; }
        public byte? IndTipoDesc { get; set; }
        [StringLength(100)]
        public string NomeServico { get; set; }
    }
}
