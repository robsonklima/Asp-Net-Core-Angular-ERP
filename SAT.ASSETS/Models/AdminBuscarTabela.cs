using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class AdminBuscarTabela
    {
        [Required]
        [Column("TABELA")]
        [StringLength(128)]
        public string Tabela { get; set; }
        [Column("COLUNA")]
        [StringLength(128)]
        public string Coluna { get; set; }
        [Column("TIPO")]
        [StringLength(128)]
        public string Tipo { get; set; }
        [Column("COLUMN_ID")]
        public int? ColumnId { get; set; }
        [Column("COLUMN_DEFAULT")]
        [StringLength(4000)]
        public string ColumnDefault { get; set; }
        [Column("IS_NULLABLE")]
        [StringLength(3)]
        public string IsNullable { get; set; }
    }
}
