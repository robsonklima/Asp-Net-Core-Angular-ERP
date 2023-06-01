using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("TempPecasConsolidado")]
    public partial class TempPecasConsolidado
    {
        [Column("cod_filial")]
        public int? CodFilial { get; set; }
        [Column("cod_item")]
        [StringLength(50)]
        public string CodItem { get; set; }
        [Column("qtd_consolidado", TypeName = "decimal(38, 2)")]
        public decimal? QtdConsolidado { get; set; }
    }
}
